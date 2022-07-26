using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class ConfirmUserEmailHandler : IQueryHandler<ConfirmUserEmailQuery,string>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcService _notificationService;

    public ConfirmUserEmailHandler(IUserRepository userRepository, INotificationGrpcService notificationService)
    {
        _userRepository = userRepository;
        _notificationService = notificationService;
    }
    
    
    public async Task<string> HandleAsync(ConfirmUserEmailQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        
        var user = await _userRepository.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        var confirmationToken = await _userRepository.GenerateEmailResetTokenAsync(user);
        await _notificationService.ResetEmailAsync(user.Email, confirmationToken);

        return confirmationToken;
    }
}