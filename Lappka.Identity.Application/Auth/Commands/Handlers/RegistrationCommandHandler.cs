using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task HandleAsync(RegistrationCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!command.Password.Equals(command.ConfirmPassword))
        {
            throw new PasswordsAreDifferentException();
        }
        
        var user = new ApplicationUser { UserName = command.Username, Email = command.Email };
        var result = await _userManager.CreateAsync(user, command.Password);
        
        
        if (!result.Succeeded)
        {
            string errors = "";
            foreach (var error in result.Errors)
            {
                errors += error.Description;
            }
            throw new UnableToRegisterUser(errors);
        }
    }
}