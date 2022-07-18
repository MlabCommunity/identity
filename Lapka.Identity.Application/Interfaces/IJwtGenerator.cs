using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;

public interface IJwtGenerator
{
    Task<string> GenerateAccessToken(AppUser user);
    Task<string> GenerateRefreshToken(AppUser user);
}