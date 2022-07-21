using System.Security.Claims;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.Auth.Queries.Handlers;

public class UseTokenQueryHandler : IQueryHandler<UseTokenQuery, TokensDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtHandler _jwtHandler;
    private readonly ITokenRepository _tokenRepository;

    public UseTokenQueryHandler(IUserRepository userRepository, IJwtHandler jwtHandler,
        ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _jwtHandler = jwtHandler;
        _tokenRepository = tokenRepository;
    }

    public async Task<TokensDto> HandleAsync(UseTokenQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var decodedToken = _jwtHandler.DecodeToken(query.AccessToken);

        var user = await _userRepository.FindByIdAsync(Guid.Parse
            (decodedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var refreshToken = await _tokenRepository.FindRefreshTokenAsync(query.RefreshToken, user.Id);

        if (refreshToken is null)
        {
            throw new RefreshTokenNotFoundException();
        }

        if (!_jwtHandler.IsExpired(refreshToken.CreatedAt))
        {
            await _tokenRepository.RemoveRefreshTokenAsync(refreshToken);
            throw new RefreshTokenExpiredException();
        }

        return new TokensDto
        {
            AccessToken = _jwtHandler.CreateAccessToken(user.Id),
            RefreshToken = refreshToken.Value
        };
    }
}