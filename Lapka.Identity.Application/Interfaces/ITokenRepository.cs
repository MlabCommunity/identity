using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;

public interface ITokenRepository
{
    Task AddRefreshToken(string token, Guid userGuid);
    Task RemoveRefreshToken(AppToken appToken);
    Task RemoveRefreshTokenByData(string token, Guid userGuid);
    Task<AppToken> GetRefreshToken(string token, Guid userGuid);
}
