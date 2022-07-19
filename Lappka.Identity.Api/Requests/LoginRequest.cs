using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public record LoginRequest
{
    [Required, EmailAddress]
    public string Email  {get; set; }
    [Required]
    public string Password {get; set; }
}