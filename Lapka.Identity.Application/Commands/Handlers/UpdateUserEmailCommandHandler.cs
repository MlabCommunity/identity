using Convey.CQRS.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;

namespace Lapka.Identity.Application.Commands.Handlers;

internal class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserInfoProvider _userInfoProvider;

    public UpdateUserEmailCommandHandler(IAppUserRepository appUserRepository, IUserInfoProvider userInfoProvider)
    {
        _appUserRepository = appUserRepository;
        _userInfoProvider = userInfoProvider;
    }

    public async Task HandleAsync(UpdateUserEmailCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userInfoProvider.GetCurrentUser();
        if (user is null)
            throw new UserNotFoundException();

        if (!_appUserRepository.CheckUsernameEmail(command.Email))
            throw new EmailAlreadyExistException(command.Email);

        user.Email = command.Email;

        await _appUserRepository.UpdateUserData(user);
    }
}