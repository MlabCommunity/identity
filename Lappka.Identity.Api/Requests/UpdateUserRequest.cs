namespace Lappka.Identity.Api.Requests;

public record UpdateUserRequest
{
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}