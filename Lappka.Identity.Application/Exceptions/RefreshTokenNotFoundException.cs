namespace Lappka.Identity.Application.Exceptions;

public class RefreshTokenNotFoundException : ProjectException
{
    public RefreshTokenNotFoundException(int errorCode = 404) : base("Refresh token not found", errorCode)
    {
    }
}