using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class UpdateUserPasswordQueryHandler : IQueryHandler<UpdateUserPasswordQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcService _notificationService;
    
    public UpdateUserPasswordQueryHandler(IUserRepository userRepository, INotificationGrpcService notificationService)
    {
        _userRepository = userRepository;
        _notificationService = notificationService;
    }
    
    public async Task<string> HandleAsync(UpdateUserPasswordQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        await _notificationService.ResetPasswordAsync(user.Email);
        return await _userRepository.GeneratePasswordResetTokenAsync(user);
    }
}