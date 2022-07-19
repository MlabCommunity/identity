using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UpdateUserEmailCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task HandleAsync(UpdateUserEmailCommand command, CancellationToken cancellationToken = new CancellationToken())
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
        
        user.Result.Email = command.Email;

        await _userManager.UpdateAsync(user.Result);
    }
}