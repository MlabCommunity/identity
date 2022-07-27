namespace Lappka.Identity.Application.Services;

public interface INotificationGrpcClient
{
    Task ResetPasswordAsync(string email,string token);
    Task ConfirmEmailAsync(string email,string token);
    Task ChangeEmailAsync(string email,string token);
}