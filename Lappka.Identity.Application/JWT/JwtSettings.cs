namespace Lappka.Identity.Application.JWT;

public class JwtSettings
{
    public string HmacSecretKey { get; set; }
    public int ExpiryDays { get; set; }
    public int ExpiryMinutes { get; set; }
    public string Issuer { get; set; }
    public bool UseRsa { get; set; }
    public string RsaPrivateKeyXML { get; set; }
    public string RsaPublicKeyXML { get; set; }        
}