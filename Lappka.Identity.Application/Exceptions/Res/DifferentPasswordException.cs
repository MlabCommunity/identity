namespace Lappka.Identity.Application.Exceptions.Res;

public class DifferentPasswordException : ProjectException
{
    public DifferentPasswordException( int errorCode = 400) : base("passwords are different", errorCode)
    {
    }
}