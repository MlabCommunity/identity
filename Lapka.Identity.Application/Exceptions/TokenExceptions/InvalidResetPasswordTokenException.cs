using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Exceptions.TokenExceptions;

public class InvalidResetPasswordTokenException : ProjectException
{
    public InvalidResetPasswordTokenException(IEnumerable<IdentityError> identityErrors)
        : base($"Token is invalid, please send reset password request again")
    {
        ExceptionData = new
        {
            SetPassword = identityErrors.Select(x => x.Description)
        };
    }
}