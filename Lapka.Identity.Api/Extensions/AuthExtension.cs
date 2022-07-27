using Lapka.Identity.Application;
using Lapka.Identity.Infrastructure;
using Lapka.Identity.Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Lapka.Identity.Api.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOption = configuration.GetOptions<JwtSettings>("JWT");
        services.AddSingleton(jwtOption);
        services.AddAuthorization();

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                opts.TokenValidationParameters = JwtParamsFactory.CreateParameters(jwtOption);
            });

        return services;
    }
}