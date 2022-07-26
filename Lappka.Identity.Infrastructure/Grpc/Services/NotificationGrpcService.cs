using Grpc.Net.Client;
using Lappka.Identity.Application.Services;


namespace Lappka.Identity.Infrastructure.Services;

public class NotificationGrpcService : INotificationGrpcService
{
    private GrpcChannel _channel = GrpcChannel.ForAddress("http://localhost:5011");
    private readonly NotificationService.NotificationServiceClient _client;


    public NotificationGrpcService()
    {
        _client = new NotificationService.NotificationServiceClient(_channel);
    }

    public async Task ResetPasswordAsync(string email, string token)
    {
        await _client.RestartPasswordAsync(new ResetPasswordRequest
        {
            Email = email,
            Token = token
        });
    }

    public async Task ConfirmEmailAsync(string email, string token)
    {
        await _client.ConfirmEmailAsync(new ConfirmEmailRequest
        {
            Email = email,
            Token = token
        });
    }

    public async Task ResetEmailAsync(string email, string token)
    {
        await _client.ResetEmailAsync(new ResetEmailRequest
        {
            Email = email,
            Token = token
        });
    }
}