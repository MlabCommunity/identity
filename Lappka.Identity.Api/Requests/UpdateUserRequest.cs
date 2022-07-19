using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public record UpdateUserRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
}