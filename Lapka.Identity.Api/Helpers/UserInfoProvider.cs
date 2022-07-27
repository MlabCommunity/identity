using System.Security.Claims;

namespace Lapka.Identity.Api.Helpers;

internal class UserInfoProvider : IUserInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInfoProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private string _id => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public Guid Id => _id is not null && Guid.TryParse(_id, out var parsedId) ? parsedId : Guid.Empty;
    public string Name => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    public string Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}