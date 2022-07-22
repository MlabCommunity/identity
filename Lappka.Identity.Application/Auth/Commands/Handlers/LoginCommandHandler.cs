using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Storage;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class LoginCommandHandler : ICommandHandler<LoginCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtHandler _jwtHandler;
    private readonly IUserRequestStorage _userRequestStorage;

    public LoginCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository, IJwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _jwtHandler = jwtHandler;
    }

    public async Task HandleAsync(LoginCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByEmailAsync(command.Email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var result = await _userRepository.LoginAsync(user, command.Password);

        if (!result.Succeeded)
        {
            throw new UnableToLoginUser();
        }

        var refreshToken = _jwtHandler.CreateRefreshToken();
        var accessToken = _jwtHandler.CreateAccessToken(user.Id);

        var appToken = new AppToken()
        {
            LoginProvider = "Lappka",
            Name = Guid.NewGuid().ToString(),
            Value = refreshToken,
            UserId = user.Id
        };

        await _tokenRepository.AddRefreshTokenAsync(appToken);
        _userRequestStorage.SetToken(command.AccessTokenCacheId, accessToken);
        _userRequestStorage.SetToken(command.RefreshTokenCacheId, refreshToken);
    }
}