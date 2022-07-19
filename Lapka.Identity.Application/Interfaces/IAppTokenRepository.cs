using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;

public interface IAppTokenRepository
{
    Task AddRefreshToken(Guid userGuid, string value, LoginProvider loginProvider = LoginProvider.Lapka);
    Task RemoveRefreshToken(AppToken appToken);
    Task RemoveRefreshTokenByData(Guid userGuid, string value, LoginProvider loginProvider = LoginProvider.Lapka);
    Task<AppToken> GetRefreshToken(Guid userGuid, string value, LoginProvider loginProvider = LoginProvider.Lapka);
}