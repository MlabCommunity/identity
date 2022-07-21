using System.Security.Claims;
using Lapka.Identity.Infrastructure.DataBase;

namespace Lapka.Identity.Api.Helpers;

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

    public Guid Id => _id is not null && Guid.TryParse(_id, out var parsedId) ? parsedId : Guid.Empty;
    public string Name => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    public string Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}

