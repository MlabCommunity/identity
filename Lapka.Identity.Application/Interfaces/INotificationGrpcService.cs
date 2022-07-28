using Lapka.Identity.Application.DTO;

namespace Lapka.Identity.Application.Interfaces;

public interface INotificationGrpcService
{
    Task SendEmailToResetPassword(string email, string token);
    Task SendEmailToResetEmailAddress(string email, string token);
    Task SendEmailToConfirmEmailAddress(ConfirmEmailAddressDTO dto);
}