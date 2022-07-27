namespace Lapka.Identity.Application.Exceptions.TokenExceptions;

public class InvalidRefreshTokenException : ProjectException
{
    public InvalidRefreshTokenException(Exception inner = null)
        : base($"Refresh token is invalid, please log in") { }
}