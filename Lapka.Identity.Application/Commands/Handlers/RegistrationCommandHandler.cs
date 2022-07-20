using Convey.CQRS.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public RegistrationCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(RegistrationCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = Guid.NewGuid();
        var user = new AppUser
        {
            Id = userId,
            UserName = command.Username,
            Email = command.EmailAddress,
            UserExtended = new UserExtended(userId, command.FirstName, command.LastName)
        };
        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            throw new InvalidRegisterDataException(result.Errors);
        }
    }
}

