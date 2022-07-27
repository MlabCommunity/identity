using Lapka.Identity.Core.Domain;
using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Core.IRepository;

public interface IAppTokenRepository
{
    Task AddRefreshToken(Guid userGuid, string value, LoginProvider loginProvider = LoginProvider.Lapka);
    Task RemoveRefreshToken(AppToken appToken);
    Task<AppToken> GetRefreshToken(Guid userGuid, string value, LoginProvider loginProvider = LoginProvider.Lapka);
}