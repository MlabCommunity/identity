using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.RequestStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lapka.Identity.Application;

public static class ApplicationExtensions
{
    public static IServiceProvider AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestStorage, RequestStorage.RequestStorage>();
        services.AddScoped<IUserRequestStorage, UserRequestStorage>();

        var builder = services.AddConvey()
            .AddCommandHandlers()
            .AddInMemoryCommandDispatcher()
            .AddQueryHandlers()
            .AddInMemoryQueryDispatcher();

        return builder.Build();
    }
}
