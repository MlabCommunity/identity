using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public class UpdateUserEmailRequest
{
    [Required, EmailAddress]
    public string Email { get; set; }
}