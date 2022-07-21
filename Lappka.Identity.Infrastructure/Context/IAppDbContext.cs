using Lappka.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Context;

public interface IAppDbContext
{
    DbSet<AppToken> Tokens { get; set; }
    DbSet<UserExtended> UsersExtended { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}