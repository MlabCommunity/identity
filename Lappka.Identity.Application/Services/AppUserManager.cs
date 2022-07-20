using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lappka.Identity.Application.Services;

public class AppUserManager : UserManager<AppUser>
{
    

    
        public AppUserManager(IUserStore<AppUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<AppUser>> logger) :
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            services, logger)
    {
    }

    public async Task<IdentityResult> CreateAsync(AppUser user,UserExtended userExtended,string password)
    {
        user.UserExtended = userExtended;
        return await base.CreateAsync(user,password);
    }


}