using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task HandleAsync(ResetPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var url = @"https://localhost:5001/Auth/setNewPassword";
        Console.WriteLine("\n\n-------TOKENN------\n" + url+"/"+token + "\n-----------\n\n");
        //todo: send mail with url + /token
        //consider: maybe verification code or sth to write in app instead?
    }
}

