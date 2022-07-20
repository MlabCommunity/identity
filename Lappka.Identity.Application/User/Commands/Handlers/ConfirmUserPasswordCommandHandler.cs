using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class ConfirmUserPasswordCommandHandler : ICommandHandler<ConfirmUserPasswordCommand>
{
    private readonly AppUserManager _appUserManager;
    
    public ConfirmUserPasswordCommandHandler(AppUserManager appUserManager)
    {
        _appUserManager = appUserManager;
    }

    public async Task HandleAsync(ConfirmUserPasswordCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.UserId is null)
        {
            throw new UserNotFoundException();
        }

        var user = await _appUserManager.FindByIdAsync(command.UserId);

        if (!command.Password.Equals(command.ConfirmationToken))
        {
            throw new DifferentPasswordException();
        }
        
        if (user is null || !user.Email.Equals(command.Email))
        {
            throw new UserNotFoundException();
        }
        await _appUserManager.ResetPasswordAsync(user, command.ConfirmationToken, command.ConfirmPassword);
    }
}