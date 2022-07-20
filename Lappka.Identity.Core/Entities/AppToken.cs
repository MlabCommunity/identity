using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Core.Entities;

public class AppToken : IdentityUserToken<string>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}