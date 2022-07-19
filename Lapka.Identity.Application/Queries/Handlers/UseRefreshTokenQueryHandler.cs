using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Queries.Handlers;

internal class UseRefreshTokenQueryHandler : IQueryHandler<UseRefreshTokenQuery, UseRefreshTokenResult>
{
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ITokenRepository _tokenRepository;

    public UseRefreshTokenQueryHandler(JwtSettings jwtSettings, IJwtGenerator jwtGenerator,
        IAppUserRepository appUserRepository, ITokenRepository tokenRepository)
    {
        _jwtSettings = jwtSettings;
        _jwtGenerator = jwtGenerator;
        _appUserRepository = appUserRepository;
        _tokenRepository = tokenRepository;
    }

    public async Task<UseRefreshTokenResult> HandleAsync(UseRefreshTokenQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(query.AccessToken);
        var idS = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid");

        if (idS is null || !Guid.TryParse(idS.Value, out var parsedId))
        {
            throw new Exception("Incorrect data");
        }

        var user = await _appUserRepository.GetUserById(parsedId);
        if (user is null)
        {
            throw new Exception("User doesn't exist");
        }

        var appToken = await _tokenRepository.GetRefreshToken(query.RefreshToken, parsedId);

        if (appToken.CreatedAt.AddMonths(_jwtSettings.RefreshExpiryMonths) < DateTime.UtcNow)
        {
            await _tokenRepository.RemoveRefreshToken(appToken);
            throw new Exception("Invalid refresh token");
        }

        var accessToken = await _jwtGenerator.GenerateAccessToken(user);
        return new UseRefreshTokenResult(accessToken);
    }
}
