using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Lappka.Identity.Application.Events;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class ConfirmChangeEmailCommandHandler : ICommandHandler<ConfirmChangeEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcClient _notificationClient;
    private readonly IBusPublisher _busPublisher;
    
    public ConfirmChangeEmailCommandHandler(IUserRepository userRepository, INotificationGrpcClient notificationClient,
        IBusPublisher busPublisher)
    {
        _userRepository = userRepository;
        _notificationClient = notificationClient;
        _busPublisher = busPublisher;
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

        var integrationEvent = new UserUpdatedEvent(user.Id, user.Email, user.UserName, user.UserExtended.FirstName,
            user.UserExtended.FirstName);

        await _busPublisher.PublishAsync(integrationEvent);
    }
}