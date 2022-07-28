using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Core.Entities;

public class AppToken : IdentityUserToken<Guid>
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}