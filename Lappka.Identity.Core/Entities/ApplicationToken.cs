using Lappka.Identity.Core.Entities.TimeStamps;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Core.Entities;

public class ApplicationToken : IdentityUserToken<string>, ICreatedAt
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}