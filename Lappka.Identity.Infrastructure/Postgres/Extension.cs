using Lappka.Identity.Infrastructure.Context;
using Lappka.Identity.Infrastructure.Postgres.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lappka.Identity.Infrastructure.Postgres;

public static class Extension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<AppDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddIdentity<IdentityUser<Guid>, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}