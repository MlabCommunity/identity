using System.IdentityModel.Tokens.Jwt;
using Lappka.Identity.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Lappka.Identity.Application.JWT;

public interface IJwtHandler
{
    string CreateRefreshToken();
    string CreateAccessToken(string userId);
    TokenValidationParameters Parameters { get;}
}