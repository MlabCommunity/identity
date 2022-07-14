using Lappka.Identity.Application.Exceptions;

namespace Lappka.Identity.Application.Exceptions;

public class UnableToRegisterUser : ProjectException
{
    public UnableToRegisterUser(string message, int errorCode = 400) : base(message, errorCode)
    {
    }
}