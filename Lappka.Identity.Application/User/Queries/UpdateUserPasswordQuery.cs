using Convey.CQRS.Queries;

namespace Lappka.Identity.Application.User.Queries;

public record UpdateUserPasswordQuery :IQuery<string>
{
    public string UserId { get; set; }
}