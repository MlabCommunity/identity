using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
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

        await _userManager.UpdateAsync(user);
    }
}