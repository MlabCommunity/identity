using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Lappka.Identity.Application.Services;

public interface IJwtHandler
{
    string CreateRefreshToken();
    string CreateAccessToken(string userId);
    TokenValidationParameters Parameters { get;}
    JwtSecurityToken DecodeToken(string token);
    public bool IsExpired(DateTime dateTime);
}