using Lapka.Identity.Core.Domain;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.DataBase;

public class DataContext : IdentityDbContext<AppUser, AppRole, Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        AppToken>
{
    public DbSet<UserExtended> UsersExtended;

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
        builder.ApplyConfigurationsFromAssembly(typeof(UserExtendedConfiguration).Assembly);

        SeedData(builder);

        base.OnModelCreating(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var adminId = Guid.NewGuid();
        builder.Entity<AppUser>().HasData(new AppUser
        {
            Id = adminId,
            UserName = "SuperAdmin",
            NormalizedUserName = "SuperAdmin".ToUpper(),
            Email = "admin@admin.com",
            NormalizedEmail = "admin@admin.com".ToUpper()
        });
        builder.Entity<UserExtended>().HasData(new UserExtended(adminId, "Super", "Admin"));

        var roleId = Guid.NewGuid();
        builder.Entity<AppRole>().HasData(
            new AppRole("SuperAdmin") { Id = roleId },
            new AppRole("Admin") { Id = Guid.NewGuid() },
            new AppRole("User") { Id = Guid.NewGuid() },
            new AppRole("Shelter") { Id = Guid.NewGuid() });

        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            UserId = adminId,
            RoleId = roleId
        });
    }


    #region ICreatedAt SaveChanges update

    private void UpdateTimestamps()
    {
        foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
        {
            if (entity.Entity is ICreatedAt created)
            {
                created.CreatedAt = DateTime.UtcNow;
            }
        }

        foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Modified))
        {
            if (entity.Entity is IModifiedAt updated)
            {
                updated.ModifiedAt = DateTime.UtcNow;
            }
        }
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    #endregion
}