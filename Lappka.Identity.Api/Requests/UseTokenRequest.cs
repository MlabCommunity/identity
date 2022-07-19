namespace Lappka.Identity.Api.Requests;

public record UseTokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}