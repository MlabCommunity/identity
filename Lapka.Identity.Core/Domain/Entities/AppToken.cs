using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Core.Domain.Entities;

public sealed class AppToken : IdentityUserToken<Guid>, ICreatedAt
{
    public DateTime CreatedAt { get; set; }
}

