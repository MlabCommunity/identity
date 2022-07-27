namespace Lapka.Identity.Application.Exceptions.TokenExceptions;

public class InvalidAccessTokenException : ProjectException
{
    public InvalidAccessTokenException(Exception inner = null)
        : base($"Access token is invalid, please log in") { }
}