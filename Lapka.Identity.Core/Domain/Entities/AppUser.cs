using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Core.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public UserExtended UserExtended { get; set; }
}

