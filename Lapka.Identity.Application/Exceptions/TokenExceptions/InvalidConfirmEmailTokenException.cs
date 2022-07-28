namespace Lapka.Identity.Application.Exceptions.TokenExceptions;

public class InvalidConfirmEmailTokenException : ProjectException
{
    public InvalidConfirmEmailTokenException(Exception inner = null)
        : base($"ConfirmEmail Token is invalid, please log in") { }
}