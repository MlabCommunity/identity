using Lappka.Identity.Core.Entities;

namespace Lappka.Identity.Core.Repositories;

public interface ITokenRepository
{
    Task AddRefreshToken(ApplicationToken appToken);
    Task RemoveRefreshToken(ApplicationToken AppToken);
    Task<ApplicationToken> GetRefreshTokenAsync(string token,string userId);
    
}