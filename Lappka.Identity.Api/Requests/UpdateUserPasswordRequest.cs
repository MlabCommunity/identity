using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public class UpdateUserPasswordRequest
{
    [Required]
    public string CurrentPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
}