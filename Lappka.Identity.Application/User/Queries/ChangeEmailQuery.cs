using Convey.CQRS.Queries;

namespace Lappka.Identity.Application.User.Queries;

public class ChangeEmailQuery :IQuery<string>
{
    public Guid UserId { get; set; }
}