using Lappka.Identity.Application.Exceptions;

public class ProjectException : Exception
{
    public ProjectException(string message, int errorCode = 400) : base(message)
    {
        _errorCode = errorCode;
    }

    public int _errorCode { get; }
}