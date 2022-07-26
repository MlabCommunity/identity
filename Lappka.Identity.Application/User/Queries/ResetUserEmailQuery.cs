using Convey.CQRS.Queries;

namespace Lappka.Identity.Application.User.Queries;

public class ConfirmUserEmailQuery :IQuery<string>
{
    public Guid UserId { get; set; }
}