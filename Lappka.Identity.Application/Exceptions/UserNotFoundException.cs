namespace Lappka.Identity.Application.Exceptions;

public class UserNotFoundException : ProjectException
{
    public UserNotFoundException(int errorCode = 400) : base("User Not Found", errorCode)
    {
    }
}