using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public class ConfirmResetPasswordCommand : ICommand
{
    public Guid UserId { get; init; }
    public string Email { get; init; }
    public string ConfirmationToken { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}