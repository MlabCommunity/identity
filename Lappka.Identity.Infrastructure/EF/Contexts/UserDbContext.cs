using Lappka.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lappka.Identity.Infrastructure.Contexts;

public class UserDbContext : DbContext
{
    
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(ConfigureUserEntity);
        
        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureUserEntity(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.UserName).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.ToTable("app_user");
    }
}