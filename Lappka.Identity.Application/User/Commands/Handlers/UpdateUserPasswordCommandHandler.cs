using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UpdateUserPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(UpdateUserPasswordCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.Id is null)
        {
            throw new UserNotFoundException();
        }

        var user = _userManager.FindByIdAsync(command.Id);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        await _userManager.ChangePasswordAsync(user.Result, command.CurrentPassword, command.NewPassword);
    }
}