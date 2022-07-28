using Lappka.Identity.Core.Entities;
using Lappka.Identity.Infrastructure.Database.Context.EntitiesConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Database.Context;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid,
    IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    AppToken>
{
    public DbSet<AppToken> Tokens { get; set; }
    public DbSet<UserExtended> UsersExtended { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var configuration = new UserExtendedConfiguration();
        builder.ApplyConfiguration<UserExtended>(configuration);

        base.OnModelCreating(builder);
    }
}