using Convey.CQRS.Queries;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Queries.Handlers;

internal class LoginQueryHandler : IQueryHandler<LoginQuery, LoginResult>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAppTokenRepository _appTokenRepository;

    public LoginQueryHandler(SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator, IAppTokenRepository appTokenRepository)
    {
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _appTokenRepository = appTokenRepository;
    }

    public async Task<LoginResult> HandleAsync(LoginQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(query.EmailAddress);
        if (user is null)
        {
            throw new InvalidLoginDataException();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);
        if (!result.Succeeded)
        {
            throw new InvalidLoginDataException();
        }

        var accessToken = await _jwtGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtGenerator.GenerateRefreshToken();

        await _appTokenRepository.AddRefreshToken(user.Id, refreshToken);

        return new LoginResult(accessToken, refreshToken);
    }
}

