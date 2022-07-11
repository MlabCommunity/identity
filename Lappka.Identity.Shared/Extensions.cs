using Lappka.Identity.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Lappka.Identity.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddHostedService<AppInitializer>();
       // services.AddScoped<ExceptionMiddleware>(); TODO: add middleware
        return services;
    }

    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    {
       // app.UseMiddleware<ExceptionMiddleware>(); TODO: add middleware
        return app;
    }
}