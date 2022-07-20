using Lapka.Identity.Core.Domain;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.DataBase;

public class DataContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid,
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
        //builder.HasDefaultSchema("identity");
        builder.ApplyConfigurationsFromAssembly(typeof(UserExtendedConfiguration).Assembly);
        base.OnModelCreating(builder);
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