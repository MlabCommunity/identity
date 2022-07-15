using System.Reflection;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lapka.Identity.Application;

public static class ApplicationExtensions
{
    public static IServiceProvider AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddFluentValidation(opt =>
        {
            opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });

        var builder = services.AddConvey()
            .AddCommandHandlers()
            .AddInMemoryCommandDispatcher()
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher();

        return builder.Build();
    }
}
