using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Storage;

namespace Lappka.Identity.Application.Auth.Queries.Handlers;

public class UseTokenQueryHandler : ICommandHandler<UseTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtHandler _jwtHandler;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRequestStorage _userRequestStorage;

    public UseTokenQueryHandler(IUserRepository userRepository, IJwtHandler jwtHandler,
        ITokenRepository tokenRepository, IUserRequestStorage userRequestStorage)
    {
        _userRepository = userRepository;
        _jwtHandler = jwtHandler;
        _tokenRepository = tokenRepository;
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

        var refreshToken = await _tokenRepository.FindRefreshTokenAsync(command.RefreshToken, user.Id);

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