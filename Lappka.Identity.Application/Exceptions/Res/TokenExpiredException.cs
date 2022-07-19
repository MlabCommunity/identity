namespace Lappka.Identity.Application.Exceptions.Res;

public class TokenExpiredException : ProjectException
{
    //TODO: check errorcode
    public TokenExpiredException(int errorCode = 400) : base("Token expired", errorCode)
    {
    }
}