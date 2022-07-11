namespace Lappka.Identity.Shared.Exceptions;

public class ProjectException : Exception
{
    protected ProjectException(string message, int errorCode = 400) : base(message)
    {
        _errorCode = errorCode;
    }

    private int _errorCode { get; }
}