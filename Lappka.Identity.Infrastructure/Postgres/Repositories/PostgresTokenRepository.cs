using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Postgres.Repositories;

internal class PostgresTokenRepository : ITokenRepository
{
    private readonly IAppDbContext _context;
    private readonly DbSet<AppToken> _tokens;

    public PostgresTokenRepository(IAppDbContext context)
    {
        _tokens = context.Tokens;
        _context = context;
    }

    public async Task AddRefreshTokenAsync(AppToken appToken)
    {
        await _tokens.AddAsync(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRefreshTokenAsync(AppToken appToken)
    {
        _tokens.Remove(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task<AppToken> FindRefreshTokenAsync(string token, Guid userId)
    {
        return await _tokens.FirstOrDefaultAsync(x => x.UserId == userId && x.Value == token);
    }
}