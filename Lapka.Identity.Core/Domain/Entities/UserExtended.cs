namespace Lapka.Identity.Core.Domain.Entities
{
    public class UserExtended : ICreatedAt, IModifiedAt
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }

        public UserExtended(Guid id, string firstName, string lastName)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
