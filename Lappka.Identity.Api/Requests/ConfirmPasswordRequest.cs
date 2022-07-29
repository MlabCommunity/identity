namespace Lappka.Identity.Api.Requests;

public record ConfirmPasswordRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmedPassword { get; init; }
    public string ConfirmationToken { get; init; }
}