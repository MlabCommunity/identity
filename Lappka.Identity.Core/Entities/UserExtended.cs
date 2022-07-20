namespace Lappka.Identity.Core.Entities;

public class UserExtended
{
    public string Id { get; protected set; }
    public AppUser User { get;  set; }
    public string FirstName { get;  set; }
    public string LastName { get; set; }
    //TODO: add additional data here

    public UserExtended(string firstName, string lastName)
    {
        Id = Guid.NewGuid().ToString();
        FirstName = firstName;
        LastName = lastName;
    }

}