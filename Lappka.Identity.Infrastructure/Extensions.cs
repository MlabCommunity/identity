using Lappka.Identity.Application.Services;
using Lappka.Identity.Infrastructure.Database;
using Lappka.Identity.Infrastructure.Exceptions;
using Lappka.Identity.Infrastructure.Grpc;
using Lappka.Identity.Infrastructure.Grpc.Services;
using Lappka.Identity.Infrastructure.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lappka.Identity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationGrpcClient,NotificationGrpcClient>();
        services.AddScoped<IUserRequestStorage, UserRequestStorage>();
        services.AddScoped<IRequestStorage, RequestStorage>();
        services.AddHostedService<DbMigrator>();
        services.AddNotification(configuration);
        services.AddPostgres(configuration);

        return services;
    }

    public static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}