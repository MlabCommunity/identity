using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class ResetPasswordQueryHandler : IQueryHandler<ResetPasswordQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcClient _notificationClient;

    public ResetPasswordQueryHandler(IUserRepository userRepository, INotificationGrpcClient notificationClient)
    {
        _userRepository = userRepository;
        _notificationClient = notificationClient;
    }

    public async Task<string> HandleAsync(ResetPasswordQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByEmailAsync(query.Email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var token = await _userRepository.GeneratePasswordResetTokenAsync(user);
        await _notificationClient.ResetPasswordAsync(user.Email, token);
        return token;
    }
}