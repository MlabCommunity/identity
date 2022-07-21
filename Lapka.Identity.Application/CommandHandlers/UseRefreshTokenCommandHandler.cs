using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.TokenExceptions;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Application.RequestStorage;
using Lapka.Identity.Core.IRepository;

namespace Lapka.Identity.Application.CommandHandlers;

internal class UseRefreshTokenCommandHandler : ICommandHandler<UseRefreshTokenCommand>
{
    private readonly JwtSettings _jwtSettings;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IAppTokenRepository _appTokenRepository;
    private readonly IUserRequestStorage _userRequestStorage;

    public UseRefreshTokenCommandHandler(JwtSettings jwtSettings, IJwtGenerator jwtGenerator,
        IAppUserRepository appUserRepository, IAppTokenRepository appTokenRepository, IUserRequestStorage userRequestStorage)
    {
        _jwtSettings = jwtSettings;
        _jwtGenerator = jwtGenerator;
        _appUserRepository = appUserRepository;
        _appTokenRepository = appTokenRepository;
        _userRequestStorage = userRequestStorage;
    }

    public async Task HandleAsync(UseRefreshTokenCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        Claim? idS = null;
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(command.AccessToken);
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

        var appToken = await _appTokenRepository.GetRefreshToken(parsedId, command.RefreshToken);

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
        _userRequestStorage.SetToken(command.TokenCasheId, accessToken);
    }
}
