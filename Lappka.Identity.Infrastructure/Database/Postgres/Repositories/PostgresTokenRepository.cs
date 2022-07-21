using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Database.Postgres.Repositories;

internal class PostgresTokenRepository : ITokenRepository
{
    private readonly AppDbContext _context;

    public PostgresTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRefreshTokenAsync(AppToken appToken)
    {
        await _context.Tokens.AddAsync(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRefreshTokenAsync(AppToken appToken)
    {
        _context.Tokens.Remove(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task<AppToken> FindRefreshTokenAsync(string token, Guid userId)
    {
        return await _context.Tokens.FirstOrDefaultAsync(x => x.UserId == userId && x.Value == token);
    }
}