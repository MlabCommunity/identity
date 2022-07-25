using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Lappka.Notification.Application;

namespace Lappka.Identity.Application.Services;

public interface INotificationGrpcService
{
    Task ResetPasswordAsync(string email);
}

public class NotificationGrpcService : INotificationGrpcService
{
    public async Task ResetPasswordAsync(string email)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5011");
        var client = new NotificationService.NotificationServiceClient(channel);

        await client.RestartPasswordAsync(new RestartPasswordRequest
        {
            Email = email
        });
    }
}