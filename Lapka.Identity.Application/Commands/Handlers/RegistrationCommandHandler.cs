using Convey.CQRS.Commands;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserExtendedRepository _userExtendedRepository;

    public RegistrationCommandHandler(UserManager<AppUser> userManager, IUserExtendedRepository userExtendedRepository)
    {
        _userManager = userManager;
        _userExtendedRepository = userExtendedRepository;
    }

    public async Task HandleAsync(RegistrationCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = new AppUser
        {
            UserName = command.UserName,
            Email = command.EmailAddress
        };
        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            var err = result.Errors.Aggregate("", (current, e) => current + e.Description + Environment.NewLine);
            throw new Exception(err);
        }

        await _userExtendedRepository.CreateUserExtended(user.Id, command.FirstName, command.LastName);
    }
}

