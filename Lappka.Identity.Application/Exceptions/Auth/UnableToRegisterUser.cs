using Lappka.Identity.Application.Exceptions;

namespace Lappka.Identity.Application.Exceptions;

public class UnableToRegisterUser : ProjectException
{
    public UnableToRegisterUser(string errors ,int errorCode = 400) : base("Unable to register user: "+errors, errorCode)
    {
    }
}