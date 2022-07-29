using Convey.CQRS.Queries;

namespace Lappka.Identity.Application.User.Queries;

public class ResetPasswordQuery : IQuery<string>
{
    public string Email { get; set; }
}