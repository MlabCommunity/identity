using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lappka.Identity.Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Lappka.Identity.Infrastructure.JWT;

public class JwtHandler : IJwtHandler
{
    private readonly JwtSettings _settings;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    private SecurityKey _issuerSigningKey;
    private SigningCredentials _signingCredentials;
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
        Parameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidIssuer = _settings.Issuer,
            IssuerSigningKey = _issuerSigningKey
        };
    }

    public JwtSecurityToken DecodeToken(string token)
    {
        return _jwtSecurityTokenHandler.ReadJwtToken(token);
    }

    public bool IsExpired(DateTime dateTime)
    {
        if (dateTime.AddDays(_settings.ExpiryDays) < DateTime.UtcNow)
        {
            return false;
        }

        return true;
    }

    public string CreateAccessToken(string userId)
    {
        var expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);
        var issuer = _settings.Issuer ?? string.Empty;
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        var jwtPayload = new JwtSecurityToken(issuer, issuer, claims, expires: expires, signingCredentials: _signingCredentials);
        var token = _jwtSecurityTokenHandler.WriteToken(jwtPayload);

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