using Grpc.Net.Client;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Lappka.Identity.Infrastructure.Grpc.Services;


//if it should be controller instead of service
public class NotificationGrpcService : INotificationGrpcService
{
    private GrpcChannel _channel;
    private readonly NotificationController.NotificationControllerClient _client;

    public NotificationGrpcService(IOptions<GrpcOptions> options)
    {
        _channel = GrpcChannel.ForAddress(options.Value
            .NotificationAddress); //TODO: ask mentor for the right way to do this
        _client = new NotificationController.NotificationControllerClient(_channel);
    }

    public async Task ResetPasswordAsync(string email, string token)
    {
        await _client.ResetPasswordAsync(new ResetPasswordRequest
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

    public async Task ChangeEmailAsync(string email, string token)
    {
        await _client.ChangeEmailAsync(new ChangeEmailRequest
        {
            Email = email,
            Token = token
        });
    }
}