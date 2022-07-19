using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public class UpdateUserPasswordCommand: ICommand
{
    public string Id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}