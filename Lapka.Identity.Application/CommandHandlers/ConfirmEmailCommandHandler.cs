using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.TokenExceptions;
using Lapka.Identity.Core.Domain.Entities;
using Lapka.Identity.Core.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.CommandHandlers;

public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailCommandHandler(IAppUserRepository appUserRepository, UserManager<AppUser> userManager)
    {
        _appUserRepository = appUserRepository;
        _userManager = userManager;
    }

    public async Task HandleAsync(ConfirmEmailCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        Claim emailClaim;
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(command.ConfirmEmailToken);
            emailClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "email");
        }
        catch (Exception e)
        {
            throw new InvalidConfirmEmailTokenException(e);
        }

        if (emailClaim is null)
        {
            throw new InvalidConfirmEmailTokenException();
        }

        var user = await _userManager.FindByEmailAsync(emailClaim.Value);
        if (!user.EmailConfirmed)
        {
            user.EmailConfirmed = true;
            await _appUserRepository.UpdateUserData(user);
        }
    }
}