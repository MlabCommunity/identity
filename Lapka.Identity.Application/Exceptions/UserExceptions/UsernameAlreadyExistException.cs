namespace Lapka.Identity.Application.Exceptions.UserExceptions;

public class UsernameAlreadyExistException : ProjectException
{
    public UsernameAlreadyExistException(string username, Exception inner = null)
        : base($"Username {username} is already taken.") { }
}