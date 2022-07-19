using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public UserExtended UserExtended { get; protected set; }
    
}