using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Database.Context;
using Lappka.Identity.Infrastructure.Database.Postgres.Repositories;
using Lappka.Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lappka.Identity.Infrastructure.Grpc;

public static class Extensions
{
    
    public static IServiceCollection AddNotification(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GrpcOptions>(configuration.GetSection("Grpc"));
        return services;
    }
}