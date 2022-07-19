using Convey.CQRS.Commands;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.IRepository;

namespace Lapka.Identity.Application.Commands.Handlers;

internal class UpdateUserDataCommandHandler : ICommandHandler<UpdateUserDataCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserInfoProvider _userInfoProvider;

    public UpdateUserDataCommandHandler(IAppUserRepository appUserRepository, IUserInfoProvider userInfoProvider)
    {
        _appUserRepository = appUserRepository;
        _userInfoProvider = userInfoProvider;
    }

    public async Task HandleAsync(UpdateUserDataCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userInfoProvider.GetCurrentUser();

        if (command.username is not null && _appUserRepository.CheckUsernameAvailability(command.username))
        {
            user.UserName = command.username;
            user.NormalizedUserName = command.username.ToUpper();
        }
        
        if(command.firstName is not null)
            user.UserExtended.FirstName = command.firstName;

        if(command.lastName is not null)
            user.UserExtended.LastName = command.lastName;

        user.UserExtended.ModifiedAt = DateTime.UtcNow;

        await _appUserRepository.UpdateUserData(user);
    }
}
