using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.NotificationExceptions;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.IRepository;

namespace Lapka.Identity.Application.CommandHandlers;

internal class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly INotificationGrpcService _notificationGrpcService;
    private readonly IJwtGenerator _jwtGenerator;

    public UpdateUserEmailCommandHandler(IAppUserRepository appUserRepository, INotificationGrpcService notificationGrpcService, IJwtGenerator jwtGenerator)
    {
        _appUserRepository = appUserRepository;
        _notificationGrpcService = notificationGrpcService;
        _jwtGenerator = jwtGenerator;
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

        var token = _jwtGenerator.GenerateNoInfoToken();

        try
        {
            await _notificationGrpcService.SendEmailToResetEmailAddress(command.Email, token); 
            //todo someday: change to noconfirmed and add endpoint to do that//}
        }
        catch(Exception ex)
        {
            throw new FailedToSendEmail(ex.Message, ex);
        }
    }
}