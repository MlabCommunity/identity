using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly INotificationGrpcService _notificationGrpcService;
    private readonly IJwtGenerator _jwtGenerator;

    public RegistrationCommandHandler(IAppUserRepository appUserRepository, INotificationGrpcService notificationGrpcService, IJwtGenerator jwtGenerator)
    {
        _appUserRepository = appUserRepository;
        _notificationGrpcService = notificationGrpcService;
        _jwtGenerator = jwtGenerator;
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

        var token = _jwtGenerator.GenerateNoInfoToken();
        await _notificationGrpcService.MailConfirmEmailAddress(user.Email, token); //todo someday: block user until email confirmed and add endpoint to do that
    }
}

