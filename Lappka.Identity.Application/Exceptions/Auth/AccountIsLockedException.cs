namespace Lappka.Identity.Application.Exceptions;

public class AccountIsLockedException : ProjectException
{
    public AccountIsLockedException(int errorCode = 400) : base("Account is locked.", errorCode)
    {
    }
}