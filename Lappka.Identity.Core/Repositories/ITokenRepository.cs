using Lappka.Identity.Core.Entities;

namespace Lappka.Identity.Core.Repositories;

public interface ITokenRepository
{
    Task AddRefreshTokenAsync(AppToken appToken);
    Task RemoveRefreshTokenAsync(AppToken appToken);
    Task<AppToken> FindRefreshTokenAsync(string token, Guid userId);
}