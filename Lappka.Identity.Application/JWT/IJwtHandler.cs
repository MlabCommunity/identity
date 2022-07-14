using Microsoft.IdentityModel.Tokens;

namespace Lappka.Identity.Application.JWT;

public interface IJwtHandler
{
    Tokens Create(string userId);
    TokenValidationParameters Parameters { get;}    
}