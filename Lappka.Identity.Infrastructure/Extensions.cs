using Lappka.Identity.Application.Services;
using Lappka.Identity.Infrastructure.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lappka.Identity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        services.AddHostedService<DbMigrator>();
        services.AddScoped<AppSignInManager>();
        services.AddScoped<AppUserManager>();
        return services;
    }
}