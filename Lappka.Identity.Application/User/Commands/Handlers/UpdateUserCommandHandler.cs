using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    
    private readonly AppUserManager _userManager;
    
    public UpdateUserCommandHandler(AppUserManager userManager)
    {
        _userManager = userManager;
    }
    
    public async Task HandleAsync(UpdateUserCommand command, CancellationToken cancellationToken = new CancellationToken())
    {

        if (command.Id is null)
        {
            throw new UserNotFoundException();
        }

        var user = await _userManager.FindByIdAsync(command.Id);
        
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        user.UserName = command.UserName;
        user.PhoneNumber = command.PhoneNumber;
        user.UserExtended.FirstName = command.FirstName;
        user.UserExtended.LastName= command.LastName;

        await _userManager.UpdateAsync(user);
    }
}