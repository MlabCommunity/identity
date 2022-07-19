

using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public class UpdateUserEmailCommand : ICommand
{
    public string Id { get; set; }
    public string Email { get; set; }
}