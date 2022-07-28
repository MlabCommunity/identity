using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Exceptions;

public class UnableToResetPassword : ProjectException
{
    public UnableToResetPassword(IdentityError[] message, int errorCode = 400) : base("Unable to reset password",
        errorCode)
    {
        ExceptionData = new
        {
            Message = message.Select(x => x.Description)
        };
    }
}