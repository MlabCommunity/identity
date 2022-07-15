using Lappka.Identity.Application.Exceptions;

namespace Lappka.Identity.Application.Exceptions;

public class PasswordsAreDifferentException : ProjectException
{
    public PasswordsAreDifferentException( int errorCode = 400) : base("passwords are different", errorCode)
    {
    }
}