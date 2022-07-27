using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Core.Domain.Entities;

public sealed class AppRole : IdentityRole<Guid>
{
    public AppRole()
    {
        
    }

    public AppRole(string roleName)
    {
        Name = roleName;
        NormalizedName = roleName.ToUpper();
    }
}

