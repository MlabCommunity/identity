using Lapka.Identity.Application.Commands;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Lapka.Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Infrastructure.Services;

internal class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUserExtendedRepository _userExtendedRepository;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserExtendedRepository userExtendedRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userExtendedRepository = userExtendedRepository;
    }

    public async Task<Guid> Register(UserRegCommand userRegCommand)
    {
        var user = new AppUser {
            UserName = userRegCommand.UserName,
            Email = userRegCommand.EmailAddress
        };
        var result = await _userManager.CreateAsync(user, userRegCommand.Password);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.ToString());
        }

        await _userExtendedRepository.CreateUserExtended(user.Id);
        
        return user.Id;
    }

    public async Task Login(UserLogCommand userLogDto)
    {
        throw new NotImplementedException();
    }
}

