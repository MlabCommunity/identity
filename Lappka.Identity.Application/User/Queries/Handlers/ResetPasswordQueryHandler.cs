using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class ResetPasswordQueryHandler : IQueryHandler<ResetPasswordQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcService _notificationService;

    public ResetPasswordQueryHandler(IUserRepository userRepository, INotificationGrpcService notificationService)
    {
        _userRepository = userRepository;
        _notificationService = notificationService;
    }

    public async Task<string> HandleAsync(ResetPasswordQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var token = await _userRepository.GeneratePasswordResetTokenAsync(user);
        await _notificationService.ResetPasswordAsync(user.Email, token);
        return token;
    }
}