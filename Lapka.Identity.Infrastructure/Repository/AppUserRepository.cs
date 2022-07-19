using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure.DataBase;

namespace Lapka.Identity.Infrastructure.Repository;

internal class AppUserRepository : IAppUserRepository
{
    private readonly DataContext _context;

    public AppUserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<AppUser> GetUserById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }
}

