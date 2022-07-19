using Convey.CQRS.Queries;
using Lappka.Identity.Application.JWT;

namespace Lappka.Identity.Application.Auth.Queries;

public record LoginQuery : IQuery<TokensResponse>
{
    public string Email  {get; set; }
    public string Password {get; set; }
}
