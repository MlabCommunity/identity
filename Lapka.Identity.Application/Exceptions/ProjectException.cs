using System.Net;

namespace Lapka.Identity.Application.Exceptions;

public class ProjectException : Exception
{
    public HttpStatusCode ErrorCode { get; }
    public object ExceptionData { get; protected set; }

    public ProjectException(string message, HttpStatusCode errorCode = HttpStatusCode.BadRequest, Exception innerException = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}