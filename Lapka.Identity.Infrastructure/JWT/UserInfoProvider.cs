using System.Security.Claims;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.JWT;

internal class UserInfoProvider : IUserInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;

    public UserInfoProvider(IHttpContextAccessor httpContextAccessor, DataContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    private string _id => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public Guid? Id => _id is not null && Guid.TryParse(_id, out var parsedId) ? parsedId : null;
    public string Name => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    public string Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

    public async Task<AppUser> GetCurrentUser()
    {
        if (Id is null)
        {
            return null;
        }

        return await _context.Users
            .Include(x => x.UserExtended)
            .FirstOrDefaultAsync(x => x.Id == Id);
    }
}

