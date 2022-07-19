using Convey.CQRS.Commands;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Commands.Handlers;

internal class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly SignInManager<AppUser> _signInManager;

    public UpdateUserPasswordCommandHandler(IAppUserRepository appUserRepository, IUserInfoProvider userInfoProvider, SignInManager<AppUser> signInManager)
    {
        _appUserRepository = appUserRepository;
        _userInfoProvider = userInfoProvider;
        _signInManager = signInManager;
    }

    public async Task HandleAsync(UpdateUserPasswordCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userInfoProvider.GetCurrentUser();
        if (user is null)
            throw new Exception("User isn't logged");

        var hashPassword = _signInManager.UserManager.PasswordHasher.HashPassword(user, command.password);

        user.PasswordHash = hashPassword;
        user.UserExtended.ModifiedAt = DateTime.UtcNow;

        await _appUserRepository.UpdateUserData(user);
    }
}
