using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.JWT;
using Microsoft.AspNetCore.Identity;


namespace Lappka.Identity.Application.Queries.Handlers;

public class LoginQueryHandler : IQueryHandler<LoginQuery, Tokens>
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtHandler _jwtHandler;

    public LoginQueryHandler(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,IJwtHandler jwtHandler)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }
    
    public async Task<Tokens> HandleAsync(LoginQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByEmailAsync(query.Email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var result = await _signInManager.PasswordSignInAsync(user,
            query.Password, query.RememberMe, lockoutOnFailure: true);
        
        if (result.Succeeded)
        {
            throw new UnableToLoginUser();
        }

        if (result.IsLockedOut)
        {
            throw new AccountIsLockedException();
        }

        var role = _userManager.GetRolesAsync(user);

        var tokens =_jwtHandler.Create(user.Id);

        return tokens;
    }
    
}