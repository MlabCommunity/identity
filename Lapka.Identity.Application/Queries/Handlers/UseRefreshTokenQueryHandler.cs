using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Exceptions.TokenExceptions;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;

namespace Lapka.Identity.Application.Queries.Handlers;

internal class UseRefreshTokenQueryHandler : IQueryHandler<UseRefreshTokenQuery, UseRefreshTokenResult>
{
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IAppTokenRepository _appTokenRepository;

    public UseRefreshTokenQueryHandler(JwtSettings jwtSettings, IJwtGenerator jwtGenerator,
        IAppUserRepository appUserRepository, IAppTokenRepository appTokenRepository)
    {
        _jwtSettings = jwtSettings;
        _jwtGenerator = jwtGenerator;
        _appUserRepository = appUserRepository;
        _appTokenRepository = appTokenRepository;
    }

    public async Task<UseRefreshTokenResult> HandleAsync(UseRefreshTokenQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        Claim? idS = null;
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(query.AccessToken);
            idS = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid");
        }
        catch (Exception e)
        {
            throw new InvalidAccessTokenException(e);
        }

        if (idS is null || !Guid.TryParse(idS.Value, out var parsedId))
        {
            throw new InvalidRefreshTokenException();
        }

        var user = await _appUserRepository.GetUserById(parsedId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var appToken = await _appTokenRepository.GetRefreshToken(parsedId, query.RefreshToken);

        if (appToken is null)
        {
            throw new InvalidRefreshTokenException();
        }

        if (appToken.CreatedAt.AddMonths(_jwtSettings.RefreshExpiryMonths) < DateTime.UtcNow)
        {
            await _appTokenRepository.RemoveRefreshToken(appToken);
            throw new InvalidRefreshTokenException();
        }

        var accessToken = await _jwtGenerator.GenerateAccessToken(user);
        return new UseRefreshTokenResult(accessToken);
    }
}
