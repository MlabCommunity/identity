using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Queries;
using Lapka.Identity.Infrastructure.Services.Interfaces;

namespace Lapka.Identity.Infrastructure.Services;

internal class AuthService : IAuthService
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AuthService(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    public async Task RegisterUser(UserRegCommand command)
    {
        await _commandDispatcher.SendAsync(command);
    }

    public async Task LoginUser(UserLogCommand command)
    {
        //await _commandDispatcher.SendAsync(command);
        throw new NotImplementedException();
    }
}

