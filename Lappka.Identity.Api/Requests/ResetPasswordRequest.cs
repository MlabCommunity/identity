namespace Lappka.Identity.Api.Requests;

public record ResetPasswordRequest
{
    public string Email { get; init; }
}