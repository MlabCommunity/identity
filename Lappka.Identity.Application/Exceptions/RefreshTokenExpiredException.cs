namespace Lappka.Identity.Application.Exceptions;

public class RefreshTokenExpiredException : ProjectException
{
    public RefreshTokenExpiredException(int errorCode = 400) : base("Refresh token expired", errorCode)
    {
    }
}