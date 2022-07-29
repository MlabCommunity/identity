namespace Lappka.Identity.Api.Requests;

public record UpdateEmailRequest

{
    public string ConfirmationToken { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; } //TODO: Add fluent validation
}