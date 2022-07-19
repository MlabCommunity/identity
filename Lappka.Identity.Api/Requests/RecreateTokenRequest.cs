namespace Lappka.Identity.Api.Requests;

public record RecreateTokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}