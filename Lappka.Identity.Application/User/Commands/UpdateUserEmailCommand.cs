using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public record UpdateUserEmailCommand : ICommand
{
    public Guid UserId { get; init; }
    public string Email { get; init; }
}