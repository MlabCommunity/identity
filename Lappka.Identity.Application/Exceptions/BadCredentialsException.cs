namespace Lappka.Identity.Application.Exceptions;

public class BadCredentialsException : ProjectException
{
    public BadCredentialsException(int errorCode = 400, Exception innerException = null) : base("Bad credentials", errorCode, innerException)
    {
    }
}