namespace Lappka.Identity.Application.Exceptions;

public class DifferentPasswordException : ProjectException
{
    public DifferentPasswordException(int errorCode = 400) : base("passwords are different", errorCode)
    {
    }
}