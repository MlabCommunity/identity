
using Convey.CQRS.Queries;

using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.JWT;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace Lappka.Identity.Application.Queries.Handlers;

public class LoginQueryHandler : IQueryHandler<LoginQuery, JwtResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtHandler _jwtHandler;

    public LoginQueryHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        IJwtHandler jwtHandler)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<JwtResponse> HandleAsync(LoginQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByEmailAsync(query.Email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var result = await _signInManager.PasswordSignInAsync(user,
            query.Password, query.RememberMe, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new UnableToLoginUser();
        }

        if (result.IsLockedOut)
        {
            throw new AccountIsLockedException();
        }

        return new JwtResponse
        {
            AccessToken = _jwtHandler.CreateAccessToken(user.Id),
            RefreshToken = _jwtHandler.CreateRefreshToken(user.Id)
        };
    }
}