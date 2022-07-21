namespace Lappka.Identity.Core.Entities;

public class UserExtended
{
    public Guid Id { get; protected set; }
    public AppUser User { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserExtended(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
    }
}