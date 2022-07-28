using Lappka.Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lappka.Identity.Infrastructure.Database.Context.EntitiesConfig;

public class UserExtendedConfiguration : IEntityTypeConfiguration<UserExtended>
{
    public void Configure(EntityTypeBuilder<UserExtended> user)
    {
        user.HasKey(x => x.Id);

        user.HasOne(x => x.User)
            .WithOne(y => y.UserExtended)
            .HasForeignKey<UserExtended>(k => k.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}