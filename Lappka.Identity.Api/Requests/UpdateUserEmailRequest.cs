namespace Lappka.Identity.Api.Requests;

public record UpdateUserEmailRequest
{
    public string Email { get; init; }
}