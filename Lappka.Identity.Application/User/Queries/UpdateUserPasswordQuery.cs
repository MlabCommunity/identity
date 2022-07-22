using Convey.CQRS.Queries;

namespace Lappka.Identity.Application.User.Queries;

public class UpdateUserPasswordQuery : IQuery<string>
{
    public Guid UserId { get; set; }
}