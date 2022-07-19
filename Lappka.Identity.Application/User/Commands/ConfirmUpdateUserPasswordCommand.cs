using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public class ConfirmUpdateUserPasswordCommand: ICommand
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string ConfirmationToken { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}