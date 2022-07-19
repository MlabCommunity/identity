using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public class ConfirmUpdateUserPasswordRequest
{
    [Required]
    public string Password { get; set; }
    [Required,EmailAddress]
    public string Email { get; set; }
    [Required]
    public string ConfiremedPassword { get; set; }
    [Required]
    public string ConfirmationToken { get; set; }
    
}