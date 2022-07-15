using System.Reflection;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Infrastructure.Context.EntitiesConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser,IdentityRole,string>
{
    public DbSet<UserExtended> UsersExtended;
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