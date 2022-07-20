using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lappka.Identity.Infrastructure.Context;

public class AppUserManager : UserManager<ApplicationUser>
{
    
    private readonly  IUserExtendedRepository _userExtendedRepository;
    
        public AppUserManager(IUserExtendedRepository userExtendedRepository,IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) :
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            services, logger)
    {
        _userExtendedRepository = userExtendedRepository;
    }

    public Task<IdentityResult> CreateAsync(ApplicationUser user,UserExtended userExtended)
    {
        _userExtendedRepository.CreateUserExtended(userExtended);
        return base.CreateAsync(user);
    }


}