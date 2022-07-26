namespace Lappka.Identity.Application.Services;

public interface INotificationGrpcService
{
    Task ResetPasswordAsync(string email);
}