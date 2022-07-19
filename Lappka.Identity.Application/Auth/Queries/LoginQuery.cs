using System.ComponentModel.DataAnnotations;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.JWT;


namespace Lappka.Identity.Application.Auth.Queries;

public record LoginQuery : IQuery<JwtResponse>
{
    [Required]
    public string Email  {get; set; }
    [Required]
    public string Password {get; set; }
    [Required]
    public bool RememberMe { get; set; }
}
