using Convey.CQRS.Queries;
using Lappka.Identity.Application.JWT;


namespace Lappka.Identity.Application.Queries;

public record LoginQuery : IQuery<JwtResponse>
{
    public string Email  {get; set; }
    public string Password {get; set; }
    public bool RememberMe { get; set; }
}
