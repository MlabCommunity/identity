using System.Security.Claims;
using Convey.CQRS.Commands;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class UseTokenCommandHandler : ICommandHandler<UseTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtHandler _jwtHandler;
    private readonly IUserRequestStorage _userRequestStorage;

    public UseTokenCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository,
        IJwtHandler jwtHandler, IUserRequestStorage userRequestStorage)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _jwtHandler = jwtHandler;
        _userRequestStorage = userRequestStorage;
    }

    public async Task HandleAsync(UseTokenCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var decodedToken = _jwtHandler.DecodeToken(command.AccessToken);

        var user = await _userRepository.FindByIdAsync(Guid.Parse
            (decodedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var refreshToken = await _tokenRepository.GetRefreshTokenAsync(command.RefreshToken, user.Id);

        if (refreshToken is null)
        {
            throw new RefreshTokenNotFoundException();
        }

        if (!_jwtHandler.IsExpired(refreshToken.CreatedAt))
        {
            await _tokenRepository.RemoveRefreshTokenAsync(refreshToken);
            throw new RefreshTokenExpiredException();
        }

        _userRequestStorage.SetToken(command.AccessTokenCacheId, _jwtHandler.CreateAccessToken(user.Id));
        _userRequestStorage.SetToken(command.RefreshTokenCacheId, refreshToken.Value);
    }
}