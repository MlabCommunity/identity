using System.IdentityModel.Tokens.Jwt;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Lapka.Identity.Infrastructure.JWT;

internal class JwtGenerator : IJwtGenerator
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtSettings _settings;

    private readonly SigningCredentials _signingCredentials;

    public JwtGenerator(SignInManager<AppUser> signInManager, JwtSettings settings)
    {
        _signInManager = signInManager;
        _settings = settings;

        _signingCredentials = JwtParamsFactory.CreateSigningCredentials(settings);
    }

    public async Task<string> GenerateToken(AppUser user)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
            Expires = DateTime.Now.AddDays(_settings.ExpiryDays),
            SigningCredentials = _signingCredentials
        };

        var handler = new JwtSecurityTokenHandler();
        var secToken = new JwtSecurityTokenHandler().CreateToken(descriptor);
        var token = handler.WriteToken(secToken);

        return token;
    }
}