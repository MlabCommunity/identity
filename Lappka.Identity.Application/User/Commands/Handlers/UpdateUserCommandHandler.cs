using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        user.PhoneNumber = command.PhoneNumber;
        user.UserExtended.FirstName = command.FirstName;
        user.UserExtended.LastName = command.LastName;

        await _userRepository.UpdateAsync(user);
    }
}