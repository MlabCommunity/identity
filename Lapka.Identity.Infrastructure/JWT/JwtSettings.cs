namespace Lapka.Identity.Infrastructure.JWT;

public class JwtSettings
{
    public int ExpiryDays { get; set; }
    public string Issuer { get; set; }
    public string RsaPrivateKeyPath { get; set; }
    public string RsaPublicKeyPath { get; set; }
}