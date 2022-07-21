using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;

namespace Lappka.Identity.Application.Auth.Queries;

public class GetTokensQuery : IQuery<TokensDto>
{
    public string Email { get; set; }
}