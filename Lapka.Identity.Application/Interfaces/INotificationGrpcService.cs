namespace Lapka.Identity.Application.Interfaces;

public interface INotificationGrpcService
{
    Task MailResetPassword(string email, string token);
    Task MailResetEmailAddress(string email, string token);
    Task MailConfirmEmailAddress(string email, string token);
}