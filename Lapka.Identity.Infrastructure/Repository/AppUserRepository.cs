using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

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

    public async Task<AppUser> GetFullUserById(Guid id)
    {
        return await _context.Users
            .Include(x => x.UserExtended)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}

