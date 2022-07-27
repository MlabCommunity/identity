using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

internal class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly SignInManager<AppUser> _signInManager;

    public UpdateUserPasswordCommandHandler(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager)
    {
        _appUserRepository = appUserRepository;
        _signInManager = signInManager;
    }

    public async Task HandleAsync(UpdateUserPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.Id == Guid.Empty)
        {
            throw new UserNotFoundException();
        }

        var user = await _appUserRepository.GetUserWithExtendedById(command.Id);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var hashPassword = _signInManager.UserManager.PasswordHasher.HashPassword(user, command.Password);

        user.PasswordHash = hashPassword;
        await _appUserRepository.UpdateUserData(user);
    }
}
