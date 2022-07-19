using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;
public interface IAppUserRepository
{
    Task<AppUser> GetUserById(Guid id);
}

