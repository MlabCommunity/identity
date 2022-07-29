using Convey.CQRS.Events;

namespace Lappka.Identity.Application.Events;

public class UserCreatedEvent : IEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string Username { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public UserCreatedEvent(Guid userId, string email, string username, string firstName, string lastName)
    {
        UserId = userId;
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }
}