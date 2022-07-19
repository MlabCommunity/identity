namespace Lappka.Identity.Application.Exceptions.Res;

public class UnableToLoginUser : ProjectException
{
    public UnableToLoginUser(int errorCode = 400) : base("Unable to login user", errorCode)
    {
    }
}