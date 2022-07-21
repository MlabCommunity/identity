using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUserRepository _appUserRepository;

    public RegistrationCommandHandler(UserManager<AppUser> userManager, IAppUserRepository appUserRepository)
    {
        _userManager = userManager;
        _appUserRepository = appUserRepository;
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

        var result = await _appUserRepository.CreateUser(user, command.Password);

        if (!result.Succeeded)
        {
            throw new InvalidRegisterDataException(result.Errors);
        }
    }
}

