

using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public class UpdateUserEmailCommand : ICommand
{
    public string UserId { get; set; }
    public string Email { get; set; }
}