using Lappka.Identity.Core.Consts;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Core.Repositories;

public interface IUserRepository
{
    Task<AppUser> FindByIdAsync(Guid userId);
    Task UpdateAsync(AppUser user);
    Task<AppUser> FindByEmailAsync(string email);
    Task<IdentityResult> RegisterAsync(AppUser user, UserExtended userExtended, string password,Role role);
    Task<SignInResult> LoginAsync(AppUser user, string password);
    Task ResetPasswordAsync(AppUser user, string confirmationToken, string password);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);

}