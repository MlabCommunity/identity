namespace Lappka.Identity.Application.Exceptions;

public class UserAlreadyExistException : ProjectException
{
    public UserAlreadyExistException(int errorCode = 400, Exception innerException = null) : base(
        "User with this email already exists", errorCode, innerException)
    {
    }
}