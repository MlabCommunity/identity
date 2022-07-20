using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;

namespace Lappka.Identity.Application.Auth.Queries;

public record UseTokenQuery : IQuery<TokensDto>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}