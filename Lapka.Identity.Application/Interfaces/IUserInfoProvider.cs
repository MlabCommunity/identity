using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;

public interface IUserInfoProvider
{
    Guid? Id { get; }
    String Name { get; }
    String Email { get; }
    Task<AppUser> CurrentUser();
}