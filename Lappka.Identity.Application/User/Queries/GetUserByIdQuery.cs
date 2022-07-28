using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;

namespace Lappka.Identity.Application.User.Queries;

public class GetUserByIdQuery : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}