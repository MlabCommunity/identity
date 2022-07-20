using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Lappka.Identity.Api.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerA(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            c.EnableAnnotations();
        });
        //services.AddFluentValidationRulesToSwagger();
        services.AddEndpointsApiExplorer();

        return services;
    }
}