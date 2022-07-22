using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Application.RequestStorage;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

internal class LoginCommandHandler : ICommandHandler<LoginCommand>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAppTokenRepository _appTokenRepository;
    private readonly IUserRequestStorage _userRequestStorage;

    public LoginCommandHandler(SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, 
        IAppTokenRepository appTokenRepository, IUserRequestStorage userRequestStorage)
    {
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _appTokenRepository = appTokenRepository;
        _userRequestStorage = userRequestStorage;
    }

    public async Task HandleAsync(LoginCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(command.EmailAddress);
        if (user is null)
        {
            throw new InvalidLoginDataException();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
        if (!result.Succeeded)
        {
            throw new InvalidLoginDataException();
        }

        var accessToken = await _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken();

        await _appTokenRepository.AddRefreshToken(user.Id, refreshToken);
        _userRequestStorage.SetToken(command.AccessTokenCacheId, accessToken);
        _userRequestStorage.SetToken(command.RefreshTokenCacheId, refreshToken);
    }
}

