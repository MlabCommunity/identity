using Convey.CQRS.Commands;
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
            throw new Exception("User is not logged in");

        if (!_appUserRepository.CheckUsernameEmail(command.email))
            throw new Exception("Email is already taken.");

        user.Email = command.email;
        user.UserExtended.ModifiedAt = DateTime.UtcNow;

        await _appUserRepository.UpdateUserData(user);
    }
}