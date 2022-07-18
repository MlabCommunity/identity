using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.Repository;

public class UserExtendedRepository : IUserExtendedRepository
{
    private readonly DataContext _context;

    public UserExtendedRepository(DataContext context)
    {
        _context = context;
    }

    public async Task CreateUserExtended(Guid id, string firstName, string lastName)
    {
        var userExtended = new UserExtended(id, firstName, lastName);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        user.UserExtended = userExtended;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateModifiedTime(Guid id)
    {
        var user = await _context.UsersExtended.FirstOrDefaultAsync(x => x.Id == id);
        user.ModifiedAt = DateTime.UtcNow;

        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserExtended(Guid id)
    {
        var user = await _context.UsersExtended.FirstOrDefaultAsync(x => x.Id == id);
        user.IsDeleted = true;

        await _context.SaveChangesAsync();
    }
}

