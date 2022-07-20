using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Exceptions.UserExceptions;

public class InvalidRegisterDataException : ProjectException
{
    public InvalidRegisterDataException(IEnumerable<IdentityError> identityErrors)
        : base($"Invalid register data.")
    {
        ExceptionData = new
        {
            Login=identityErrors.Select(x=>x.Description)
        };
    }
}