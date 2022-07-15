namespace Lappka.Identity.Core.Entities;

public class UserExtended
{
    public string Id { get; protected set; }
    public ApplicationUser User { get; protected set; }
    //TODO: add additional data here

    public UserExtended()
    {
        Id = Guid.NewGuid().ToString();
    }
}