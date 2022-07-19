namespace Lappka.Identity.Application.JWT;

public class JwtSettings
{
    public string HmacSecretKey { get; set; }
    public int ExpiryMinutes { get; set; }
    public int ExpiryDays { get; set; }
    public string Issuer { get; set; }
    public bool UseRsa { get; set; }

    public string RsaPrivateKeyPath { get; set; }
    public string RsaPublicKeyPath { get; set; }   
}