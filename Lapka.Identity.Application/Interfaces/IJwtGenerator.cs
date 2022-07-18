using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;

public interface IJwtGenerator
{
    Task<string> GenerateToken(AppUser user);
}