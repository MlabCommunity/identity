using Lappka.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lappka.Identity.Infrastructure.Context;

public interface IAppDbContext
{
     DbSet<UserExtended> UsersExtended { get; set; }
     DbSet<AppToken> Tokens { get; set; }
     Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}