using Lapka.Identity.Application.DTO;
using Lapka.Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Infrastructure.Services;

internal class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<string> Register(UserRegDto userRegDto)
    {
        var user = new IdentityUser { UserName = userRegDto.UserName, Email = userRegDto.UserName };
        var result = await _userManager.CreateAsync(user, userRegDto.Password);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.ToString());
        }

        return user.Id;
    }

    public async Task Login(UserLogDto userLogDto)
    {
        throw new NotImplementedException();
    }
}

