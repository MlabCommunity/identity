using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Infrastructure.DataBase;
using Lapka.Identity.Infrastructure.Exceptions;
using Lapka.Identity.Infrastructure.JWT;
using Lapka.Identity.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lapka.Identity.Infrastructure;

public static class InfrastuctureExtensions
{
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<DbMigrator>();
        services.AddScoped<ExceptionMiddleware>();
        services.AddScoped<IJwtGenerator, JwtGenerator>(); // singleton?
        services.AddScoped<IUserInfoProvider, UserInfoProvider>();
        services.AddScoped<IAppTokenRepository, AppTokenRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();

        return services;
    }

    public static IApplicationBuilder UseInfrastucture(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
