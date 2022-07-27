using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class ConfirmResetPasswordCommandHandler : ICommandHandler<ConfirmResetPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ConfirmResetPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(ConfirmResetPasswordCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(command.UserId);

        if (!command.Password.Equals(command.ConfirmationToken))
        {
            throw new DifferentPasswordException();
        }

        if (user is null || !user.Email.Equals(command.Email))
        {
            throw new UserNotFoundException();
        }

        await _userRepository.ResetPasswordAsync(user, command.ConfirmationToken, command.Password);
    }
}