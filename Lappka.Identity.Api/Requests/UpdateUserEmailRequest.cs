namespace Lappka.Identity.Api.Requests;

public record UpdateUserEmailRequest

{
    public string ConfirmationToken { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmedPassword { get; init; } //TODO: Add fluent validation
}