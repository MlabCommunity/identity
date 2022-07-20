using System.Net;

namespace Lapka.Identity.Application.Exceptions.UserExceptions;

public class UserNotFoundException : ProjectException
{
    public UserNotFoundException(Exception inner = null)
        : base($"User not found", HttpStatusCode.NotFound) { }
}