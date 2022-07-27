using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class ChangeUserEmailQueryHandler : IQueryHandler<ChangeEmailQuery,string>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcService _notificationService;

    public ChangeUserEmailQueryHandler(IUserRepository userRepository, INotificationGrpcService notificationService)
    {
        _userRepository = userRepository;
        _notificationService = notificationService;
    }
    
    
    public async Task<string> HandleAsync(ChangeEmailQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        
        var user = await _userRepository.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        var confirmationToken = await _userRepository.GenerateEmailResetTokenAsync(user);
        await _notificationService.ChangeEmailAsync(user.Email, confirmationToken);

        return confirmationToken;
    }
}