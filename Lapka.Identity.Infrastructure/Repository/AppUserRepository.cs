﻿using Lapka.Identity.Application.Interfaces;
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

    public async Task UpdateUserData(AppUser user)
    {
        if (user.UserExtended is not null)
        {
            user.UserExtended.ModifiedAt = DateTime.UtcNow;
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public bool CheckUsernameAvailability(string username)
    {
        return _context.Users.Count(x => x.NormalizedUserName == username.ToUpper()) <= 0;
    }

    public bool CheckUsernameEmail(string email)
    {
        return _context.Users.Count(x => x.Email == email) <= 0;
    }
}

