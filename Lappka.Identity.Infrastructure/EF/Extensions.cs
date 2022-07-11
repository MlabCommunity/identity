using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Contexts;
using Lappka.Identity.Infrastructure.EF.Options;
using Lappka.Identity.Infrastructure.EF.Repository;
using Lappka.Identity.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lappka.Identity.Infrastructure.EF;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();

        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<UserDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));
        
        return services;
    }
}