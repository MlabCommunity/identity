using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Lappka.Identity.Application.JWT;

public interface IJwtHandler
{
    string CreateRefreshToken(string userId);
    string CreateAccessToken(string userId);
    TokenValidationParameters Parameters { get;}
}