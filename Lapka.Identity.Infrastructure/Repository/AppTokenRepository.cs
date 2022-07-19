using Lapka.Identity.Application;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.Repository;

internal class AppTokenRepository : IAppTokenRepository
{
    private readonly DataContext _context;

    public AppTokenRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddRefreshToken(Guid userGuid, string value, LoginProvider loginProvider)
    {
        var token = new AppToken()
        {
            LoginProvider = loginProvider.ToString(),
            Name = Guid.NewGuid().ToString()[..8],
            UserId = userGuid,
            Value = value
        };

        await _context.UserTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRefreshToken(AppToken appToken)
    {
        _context.UserTokens.Remove(appToken);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRefreshTokenByData(Guid userGuid, string value, LoginProvider loginProvider)
    {
        var tokenObject = await this.GetRefreshToken(userGuid, value,  loginProvider);
        _context.UserTokens.Remove(tokenObject);
        await _context.SaveChangesAsync();
    }

    public async Task<AppToken> GetRefreshToken(Guid userGuid, string value, LoginProvider loginProvider)
    {
        return await _context.UserTokens
            .FirstOrDefaultAsync(x => x.UserId == userGuid && x.Value == value && x.LoginProvider == loginProvider.ToString());
    }
}

