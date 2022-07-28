using System.Net;

namespace Lapka.Identity.Application.Exceptions.NotificationExceptions;

public class FailedToSendEmail : ProjectException
{
    public FailedToSendEmail(string description, Exception inner = null)
        : base($"Failed to send an email. " + description, HttpStatusCode.InternalServerError) { }
}