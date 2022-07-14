using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    
    private readonly UserManager<IdentityUser> _userManager;

    public RegistrationCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task HandleAsync(RegistrationCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!command.Password.Equals(command.ConfirmPassword))
        {
            throw new PasswordsAreDifferentException();
        }
        
        var user = new IdentityUser { UserName = command.UserName, Email = command.Email };
        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            throw new UnableToRegisterUser(result.Errors.ToString());
        }
    }
}