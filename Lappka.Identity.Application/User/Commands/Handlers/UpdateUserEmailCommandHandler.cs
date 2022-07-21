using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(UpdateUserEmailCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.Email = command.Email;

        await _userRepository.UpdateAsync(user);
    }
}