using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Exceptions;

public class UnableToRegisterUser : ProjectException
{
    public UnableToRegisterUser(IdentityError[] message, int errorCode = 400) : base("Unable to register user",
        errorCode)
    {
        ExceptionData = new
        {
            Message = message.Select(x => x.Description)
        };
    }
}