using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.TokenExceptions;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

public class SetNewPasswordCommandHandler : ICommandHandler<SetNewPasswordCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public SetNewPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(SetNewPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var resetPassResult = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);
        if (!resetPassResult.Succeeded)
        {
            throw new InvalidResetPasswordTokenException(resetPassResult.Errors.ToList());
        }

    }
}
