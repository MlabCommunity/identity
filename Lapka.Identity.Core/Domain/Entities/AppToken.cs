using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Core.Domain.Entities;

public class AppToken : IdentityUserToken<Guid>, ICreatedAt
{
    public DateTime CreatedAt { get; set; } 
}

