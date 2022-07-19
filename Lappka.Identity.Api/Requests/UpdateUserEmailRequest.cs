using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public class UpdateUserEmailRequest
{
    [Required]
    public string Email { get; set; }
}