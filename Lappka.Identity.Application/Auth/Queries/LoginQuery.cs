using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;

namespace Lappka.Identity.Application.Auth.Queries;

public record LoginQuery : IQuery<TokensDto>
{
    public string Email  {get; set; }
    public string Password {get; set; }
}
