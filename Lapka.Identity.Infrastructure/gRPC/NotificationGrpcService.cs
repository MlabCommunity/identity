using Grpc.Net.Client;
using Lapka.Identity.Application.Interfaces;

namespace Lapka.Identity.Infrastructure.gRPC;

internal class NotificationGrpcService : INotificationGrpcService
{
    private readonly GrpcChannel _channel;
    private readonly NotificationService.NotificationServiceClient _notificationClient;

    public NotificationGrpcService(GrpcSettings grpcSettings)
    {
        _channel = GrpcChannel.ForAddress(grpcSettings.NotificationServerAddress);
        _notificationClient = new NotificationService.NotificationServiceClient(_channel);
    }

    public async Task MailResetPassword(string email, string token)
    {
        _ = await _notificationClient.ResetPasswordAsync(new ResetPasswordRequest()
        {
            Email = email,
            Token = token
        });
    }

    public async Task MailResetEmailAddress(string email, string token)
    {
        _ = await _notificationClient.ResetEmailAsync(new ResetEmailRequest()
        {
            Email = email,
            Token = token
        });
    }

    public async Task MailConfirmEmailAddress(string email, string token)
    {
        _ = await _notificationClient.ConfirmEmailAsync(new ConfirmEmailRequest()
        {
            Email = email,
            Token = token
        });
    }
}