using Convey.CQRS.Queries;
using Lappka.Identity.Application.JWT;

namespace Lappka.Identity.Application.Auth.Queries;

public record UseTokenQuery : IQuery<TokensResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}