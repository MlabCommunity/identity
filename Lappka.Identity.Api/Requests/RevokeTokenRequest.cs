namespace Lappka.Identity.Api.Requests;

public record RevokeTokenRequest
{
    public string RefreshToken { get; init; }
}