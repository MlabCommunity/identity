using System.ComponentModel.DataAnnotations;

namespace Lappka.Identity.Api.Requests;

public record RevokeTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}