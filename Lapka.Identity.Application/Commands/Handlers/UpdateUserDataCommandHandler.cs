using Convey.CQRS.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;

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

        if(command.Username is not null)
        {
            if (_appUserRepository.CheckUsernameAvailability(command.Username))
            {
                user.UserName = command.Username;
                user.NormalizedUserName = command.Username.ToUpper();
            }
            else
            {
                throw new UsernameAlreadyExistException(command.Username);
            }
        }
        
        if(command.FirstName is not null)
            user.UserExtended.FirstName = command.FirstName;

        if(command.LastName is not null)
            user.UserExtended.LastName = command.LastName;

        await _appUserRepository.UpdateUserData(user);
    }
}
