namespace Lappka.Identity.Application.Exceptions;

public class ProjectException : Exception
{
    public int _errorCode { get; }
    public object ExceptionData { get; protected set; }

    public ProjectException(string message, int errorCode = 400, Exception innerException = null) : base(message,
        innerException)
    {
        _errorCode = errorCode;
    }
}