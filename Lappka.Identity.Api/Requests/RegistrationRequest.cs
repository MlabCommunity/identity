using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public record RegistrationRequest
{
    [Required]
    public string Username { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}
