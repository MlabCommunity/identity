using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Lappka.Identity.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Postgres.Repositories;

internal class PostgresTokenRepository : ITokenRepository
{
    private readonly IAppDbContext _context;
    private readonly DbSet<ApplicationToken> _tokens;

    public PostgresTokenRepository(IAppDbContext context)
    {
        _tokens = _context.Tokens;
        _context = context;
    }

    public async Task AddRefreshToken(ApplicationToken appToken)
    {
        await _tokens.AddAsync(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRefreshToken(ApplicationToken appToken)
    {
        _tokens.Remove(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task<ApplicationToken> GetRefreshTokenAsync(string token, string userId)
    {
        return await _tokens.FirstOrDefaultAsync(x => x.UserId == userId && x.Value == token);
    }
}