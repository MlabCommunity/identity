using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class ConfirmChangeEmailCommandHandler : ICommandHandler<ConfirmChangeEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcService _notificationService;

    
    public ConfirmChangeEmailCommandHandler(IUserRepository userRepository, INotificationGrpcService notificationService)
    {
        _userRepository = userRepository;
        _notificationService = notificationService;
    }
    
    public async Task HandleAsync(ConfirmChangeEmailCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var isCorrect = await _userRepository.CheckPasswordAsync(user, command.Password);

        if (!isCorrect)
        {
            throw new BadCredentialsException();
        }

        user.Email = command.Email;
        var result = await _userRepository.ChangeEmailAsync(user, command.ConfirmationToken, command.Password);

        if (!result.Succeeded)
        {
            throw new UnableToResetEmail(result.Errors.ToArray());
        }
    }
}