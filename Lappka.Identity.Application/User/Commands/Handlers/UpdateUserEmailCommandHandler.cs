using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;

namespace Lappka.Identity.Application.User.Commands.Handlers;

public class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    
    private readonly AppUserManager _userManager;
    
    public UpdateUserEmailCommandHandler(AppUserManager userManager)
    {
        _userManager = userManager;
    }
    
    public async Task HandleAsync(UpdateUserEmailCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.UserId is null)
        {
            throw new UserNotFoundException();
        }

        var user = await _userManager.FindByIdAsync(command.UserId);
        
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        user.Email = command.Email;

        await _userManager.UpdateAsync(user);
    }
}