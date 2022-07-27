using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Exceptions;

public class UnableToResetEmail : ProjectException
{
    public UnableToResetEmail(IdentityError[] message, int errorCode = 400) : base("Unable to reset email",
        errorCode)
    {
        ExceptionData = new
        {
            Message = message.Select(x => x.Description)
        };
    }
    
    
}