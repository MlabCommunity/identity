using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public record UpdateUserRequest
{

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}