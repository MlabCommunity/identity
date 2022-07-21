namespace Lappka.Identity.Api.Requests;

public record UseTokenRequest
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}