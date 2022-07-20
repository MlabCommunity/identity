using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Core.Entities;

public class AppUser : IdentityUser
{
    public UserExtended UserExtended { get; set; }
    
}