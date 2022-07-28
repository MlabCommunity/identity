using Grpc.Net.Client;
using Lapka.Identity.Application.DTO;
using Lapka.Identity.Application.Interfaces;

namespace Lapka.Identity.Infrastructure.gRPC;

internal class NotificationGrpcClient : INotificationGrpcService
{
    private readonly GrpcChannel _channel;
    private readonly NotificationService.NotificationServiceClient _notificationClient;

    public NotificationGrpcClient(GrpcSettings grpcSettings)
    {
        _channel = GrpcChannel.ForAddress(grpcSettings.NotificationServerAddress);
        _notificationClient = new NotificationService.NotificationServiceClient(_channel);
    }

    public async Task SendEmailToResetPassword(string email, string token)
    {
        _ = await _notificationClient.ResetPasswordAsync(new ResetPasswordRequest()
        {
            Email = email,
            Token = token
        });
    }

    public async Task SendEmailToResetEmailAddress(string email, string token)
    {
        _ = await _notificationClient.ResetEmailAsync(new ResetEmailRequest()
        {
            Email = email,
            Token = token
        });
    }

    public async Task SendEmailToConfirmEmailAddress(ConfirmEmailAddressDTO dto)
    {
        _ = await _notificationClient.ConfirmEmailAsync(new ConfirmEmailRequest()
        {
            Email = dto.Email,
            Token = dto.Token,
            Username = dto.Username,
            Firstname = dto.FirstName,
            Lastname = dto.LastName,
            Userid = dto.UserId.ToString()
        });
    }
}