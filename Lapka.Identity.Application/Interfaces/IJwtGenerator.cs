using Lapka.Identity.Core.Domain.Entities;

namespace Lapka.Identity.Application.Interfaces;

public interface IJwtGenerator
{
    Task<string> GenerateAccessToken(AppUser user);
    string GenerateNoInfoToken();
    string GenerateConfirmEmailToken(string email);
}