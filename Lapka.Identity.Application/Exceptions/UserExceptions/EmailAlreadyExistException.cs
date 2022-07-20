namespace Lapka.Identity.Application.Exceptions.UserExceptions;

public class EmailAlreadyExistException : ProjectException
{
    public EmailAlreadyExistException(string email, Exception inner = null)
        : base($"Password address {email} is already taken.") { }
}