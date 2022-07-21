namespace Lappka.Identity.Application.Exceptions;

public class UnableToLoginUser : ProjectException
{
    public UnableToLoginUser(int errorCode = 400) : base("Unable to login user", errorCode)
    {
    }
}