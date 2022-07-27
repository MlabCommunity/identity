using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using static Lappka.Identity.Core.Consts.Role;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationGrpcClient _notificationClient;


    public RegistrationCommandHandler(IUserRepository userRepository, INotificationGrpcClient notificationClient)
    {
        _userRepository = userRepository;
        _notificationClient = notificationClient;
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

        var token = await _userRepository.GenerateEmailConfirmationTokenAsync(user);

        await _notificationClient.ConfirmEmailAsync(user.Email, token);
    }
}