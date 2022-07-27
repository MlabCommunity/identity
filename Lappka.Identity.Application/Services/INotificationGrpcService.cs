namespace Lappka.Identity.Application.Services;

public interface INotificationGrpcService
{
    Task ResetPasswordAsync(string email,string token);
    Task ConfirmEmailAsync(string email,string token);
    Task ChangeEmailAsync(string email,string token);
}