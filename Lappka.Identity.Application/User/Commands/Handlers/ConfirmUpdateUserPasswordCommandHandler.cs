using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserPasswordCommandHandler : ICommandHandler<ConfirmUpdateUserPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UpdateUserPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(ConfirmUpdateUserPasswordCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.UserId is null)
        {
            throw new UserNotFoundException();
        }

        var user = await _userManager.FindByIdAsync(command.UserId);

        if (!command.Password.Equals(command.ConfirmationToken))
        {
            throw new DifferentPasswordException();
        }
        
        if (user is null || !user.Email.Equals(command.Email))
        {
            throw new UserNotFoundException();
        }

        await _userManager.ResetPasswordAsync(user, command.ConfirmationToken, command.ConfirmPassword);
    }
}