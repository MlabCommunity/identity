using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Lappka.Identity.Application.Events;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IBusPublisher _busPublisher;

    public UpdateUserCommandHandler(IUserRepository userRepository, IBusPublisher busPublisher)
    {
        _userRepository = userRepository;
        _busPublisher = busPublisher;
    }

    public async Task HandleAsync(UpdateUserCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.UserName = command.UserName;
        user.UserExtended.FirstName = command.FirstName;
        user.UserExtended.LastName = command.LastName;

        await _userRepository.UpdateAsync(user);
        
        var integrationEvent = new UserUpdatedEvent(user.Id, user.Email, user.UserName, user.UserExtended.FirstName,
            user.UserExtended.LastName);

        await _busPublisher.PublishAsync(integrationEvent);
    }
}