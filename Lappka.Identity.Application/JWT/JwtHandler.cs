using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Lappka.Identity.Application.JWT;

public class JwtHandler : IJwtHandler
{
    private readonly JwtSettings _settings;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    private SecurityKey _issuerSigningKey;
    private SigningCredentials _signingCredentials;
    private JwtHeader _jwtHeader;
    public TokenValidationParameters Parameters { get; private set; }


    public JwtHandler(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        if (_settings.UseRsa)
        {
            InitializeRsa();
        }
        else
        {
            InitializeHmac();
        }

        InitializeJwtParameters();
    }

    private void InitializeRsa()
    {
        RSA publicRsa = RSA.Create();
        var publicKeyXml = File.ReadAllText(_settings.RsaPublicKeyPath);
        publicRsa.ImportFromPem(publicKeyXml);
        _issuerSigningKey = new RsaSecurityKey(publicRsa);

        if (string.IsNullOrWhiteSpace(_settings.RsaPrivateKeyPath))
        {
            return;
        }

        RSA privateRsa = RSA.Create();
        var privateKeyXml = File.ReadAllText(_settings.RsaPrivateKeyPath);
        privateRsa.ImportFromPem(privateKeyXml);
        var privateKey = new RsaSecurityKey(privateRsa);
        _signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
    }

    private void InitializeHmac()
    {
        _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.HmacSecretKey));
        _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
    }

    private void InitializeJwtParameters()
    {
        _jwtHeader = new JwtHeader(_signingCredentials);
        Parameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidIssuer = _settings.Issuer,
            IssuerSigningKey = _issuerSigningKey
        };
    }

    public string CreateAccessToken(string userId)
    {
        var halo = DateTime.Now.AddMinutes(_settings.ExpiryMinutes); //tutaj

        var issuer = _settings.Issuer ?? string.Empty;

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        var x = new JwtSecurityToken(issuer, issuer, claims, expires: halo, signingCredentials: _signingCredentials);
        /*var payload = new JwtPayload
        {
            { "sub", userId },
            { "unique_name", userId },
            { "iss", issuer },
            { "iat", now },
            { "nbf", now },
            { "exp", exp },
        };
        */
        var token = _jwtSecurityTokenHandler.WriteToken(x);

        return token;
    }

    public string CreateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}