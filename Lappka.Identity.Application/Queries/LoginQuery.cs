using Convey.CQRS.Queries;


namespace Lappka.Identity.Application.Queries;

public record LoginQuery : IQuery<JWT.Tokens>
{
    public string Email  {get; set; }
    public string Password {get; set; }
    public bool RememberMe { get; set; }
}
