using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly INotificationGrpcService _notificationGrpcService;

    public ResetPasswordCommandHandler(UserManager<AppUser> userManager, INotificationGrpcService notificationGrpcService)
    {
        _userManager = userManager;
        _notificationGrpcService = notificationGrpcService;
    }

    public async Task HandleAsync(ResetPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        await _notificationGrpcService.MailResetPassword(command.Email, token);
    }
}