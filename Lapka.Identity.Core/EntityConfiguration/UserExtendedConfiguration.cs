using Lapka.Identity.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lapka.Identity.Core.EntityConfiguration
{
    public class UserExtendedConfiguration : IEntityTypeConfiguration<UserExtended>
    {
        public void Configure(EntityTypeBuilder<UserExtended> user)
        {
            user.HasKey(x => x.Id);

            user.Property(x => x.IsDeleted)
                .IsRequired();

            user.HasOne(x => x.User)
                .WithOne(y => y.UserExtended)
                .HasForeignKey<UserExtended>(k => k.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
