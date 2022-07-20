using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Context;
using Lappka.Identity.Infrastructure.Postgres.Options;
using Lappka.Identity.Infrastructure.Postgres.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lappka.Identity.Infrastructure.Postgres;

public static class Extension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenRepository, PostgresTokenRepository>();
        
        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<AppDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IAppDbContext, AppDbContext>();
        
        
        return services;
    }
}