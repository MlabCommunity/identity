using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    
    private readonly AppUserManager _appUserManager;
    
    public RegistrationCommandHandler(AppUserManager appUserManager)
    {
        _appUserManager = appUserManager;
    }
    
    public async Task HandleAsync(RegistrationCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!command.Password.Equals(command.ConfirmPassword))
        {
            throw new DifferentPasswordException();
        }
        
        var user = new AppUser { UserName = command.Username, Email = command.Email };
        var userExtended = new UserExtended(command.FirstName, command.LastName);
        var result = await _appUserManager.CreateAsync(user,userExtended, command.Password);
        
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