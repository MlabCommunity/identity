using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.Repository;

internal class TokenRepository : ITokenRepository
{
    private readonly DataContext _context;

    public TokenRepository(DataContext context)
    {
        _context = context;
    }

    public Task AddRefreshToken(string token, Guid userGuid)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveRefreshToken(AppToken appToken)
    {
        _context.UserTokens.Remove(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRefreshTokenByData(string token, Guid userGuid)
    {
        var tokenObject = await this.GetRefreshToken(token, userGuid);
        _context.UserTokens.Remove(tokenObject);
        await _context.SaveChangesAsync();
    }

    public async Task<AppToken> GetRefreshToken(string token, Guid userGuid)
    {
        return await _context.UserTokens
            .FirstOrDefaultAsync(x => x.UserId == userGuid && x.Value == token);
    }
}

