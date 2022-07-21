using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Queries;

public record LoginCommand : ICommand
{
    public string Email { get; init; }
    public string Password { get; init; }
}