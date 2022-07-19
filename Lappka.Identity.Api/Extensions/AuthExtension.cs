using Lappka.Identity.Application.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Lappka.Identity.Api.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("jwt"));
        services.AddSingleton<IJwtHandler, JwtHandler>();
        services.AddAuthorization();
        
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        var jwtHandler = serviceProvider.GetService<IJwtHandler>();

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                opts => { opts.TokenValidationParameters = jwtHandler.Parameters; });

        return services;
    }    
}

