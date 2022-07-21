using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.IRepository;

namespace Lapka.Identity.Application.CommandHandlers;

internal class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    private readonly IAppUserRepository _appUserRepository;

    public UpdateUserEmailCommandHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task HandleAsync(UpdateUserEmailCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.Id == Guid.Empty)
        {
            throw new UserNotFoundException();
        }

        if (!_appUserRepository.CheckUsernameEmail(command.Email))
        {
            throw new EmailAlreadyExistException(command.Email);
        }

        var user = await _appUserRepository.GetUserWithExtendedById(command.Id);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.Email = command.Email;

        await _appUserRepository.UpdateUserData(user);
    }
}