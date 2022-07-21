using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using static Lappka.Identity.Core.Consts.Role;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    private readonly IUserRepository _userRepository;

    public RegistrationCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task HandleAsync(RegistrationCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (!command.Password.Equals(command.ConfirmPassword))
        {
            throw new DifferentPasswordException();
        }

        var userExist = await _userRepository.FindByEmailAsync(command.Email);

        if (userExist != null)
        {
            throw new UserAlreadyExistException();
        }
        
        var user = new AppUser { UserName = command.Username, Email = command.Email };
        var userExtended = new UserExtended(command.FirstName, command.LastName);
        var result = await _userRepository.RegisterAsync(user, userExtended, command.Password, USER);

        if (!result.Succeeded)
        {
            throw new UnableToRegisterUser(result.Errors.ToArray());
        }
    }
}