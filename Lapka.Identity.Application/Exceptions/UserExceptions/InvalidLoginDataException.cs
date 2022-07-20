namespace Lapka.Identity.Application.Exceptions.UserExceptions;

public class InvalidLoginDataException : ProjectException
{
    public InvalidLoginDataException(Exception inner = null)
        : base($"Invalid login data.") { }
}