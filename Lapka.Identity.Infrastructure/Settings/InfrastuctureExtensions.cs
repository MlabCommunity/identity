using Lapka.Identity.Core.IRepository;
using Lapka.Identity.Infrastructure.Exceptions;
using Lapka.Identity.Infrastructure.Repository;
using Lapka.Identity.Infrastructure.Services;
using Lapka.Identity.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lapka.Identity.Infrastructure.Settings;

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
        services.AddScoped<ExceptionMiddleware>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserExtendedRepository, UserExtendedRepository>();

        return services;
    }

    public static IApplicationBuilder UseInfrastucture(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
