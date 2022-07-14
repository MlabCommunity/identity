namespace Lapka.Identity.Core.Domain.Entities
{
    public class UserExtended : ICreatedAt, IModifiedAt
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }

        public UserExtended(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
