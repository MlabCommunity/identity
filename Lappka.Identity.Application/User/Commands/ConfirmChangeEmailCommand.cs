using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public record ConfirmChangeEmailCommand : ICommand
{
    public Guid UserId { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmationToken { get; init; }
}