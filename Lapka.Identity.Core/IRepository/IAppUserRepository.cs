using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Core.IRepository;
public interface IAppUserRepository
{
    Task<AppUser> GetUserById(Guid id);
    Task<AppUser> GetUserWithExtendedById(Guid id);
    Task<IdentityResult> CreateUser(AppUser user, string password);
    Task UpdateUserData(AppUser user);
    bool CheckUsernameAvailability(string username);
    bool CheckUsernameEmail(string email);
}

