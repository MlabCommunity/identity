using Lappka.Identity.Core.Entities;

namespace Lappka.Identity.Core.Repositories;

public interface ITokenRepository
{
    Task AddRefreshToken(AppToken appToken);
    Task RemoveRefreshToken(AppToken AppToken);
    Task<AppToken> GetRefreshTokenAsync(string token,string userId);
    
}