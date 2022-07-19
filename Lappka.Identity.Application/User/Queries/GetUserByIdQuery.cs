using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;

namespace Lappka.Identity.Application.User.Queries;

public record GetUserByIdQuery : IQuery<UserDto>
{
    public string Id;
}