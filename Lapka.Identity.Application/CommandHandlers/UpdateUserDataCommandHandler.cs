using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.IRepository;

namespace Lapka.Identity.Application.CommandHandlers;

internal class UpdateUserDataCommandHandler : ICommandHandler<UpdateUserDataCommand>
{
    private readonly IAppUserRepository _appUserRepository;

    public UpdateUserDataCommandHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task HandleAsync(UpdateUserDataCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.Id == Guid.Empty)
        {
            throw new UserNotFoundException();
        }

        var user = await _appUserRepository.GetUserWithExtendedById(command.Id);

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
