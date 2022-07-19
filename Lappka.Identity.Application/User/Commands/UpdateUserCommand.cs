using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.User.Commands;

public class UpdateUserCommand : ICommand
{
    public string Id { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
}
