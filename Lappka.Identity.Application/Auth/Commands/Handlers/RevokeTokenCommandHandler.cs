using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand>
{
    private readonly AppUserManager _appUserManager;
    private readonly IJwtHandler _jwtHandler;
    private readonly ITokenRepository _tokenRepository;

    public RevokeTokenCommandHandler(AppUserManager appUserManager,IJwtHandler jwtHandler,ITokenRepository tokenRepository)
    {
        _appUserManager = appUserManager;
        _jwtHandler = jwtHandler;
        _tokenRepository = tokenRepository;
    }
    
    public async Task HandleAsync(RevokeTokenCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        
        var user = await _appUserManager.FindByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        var refreshToken = await _tokenRepository.GetRefreshTokenAsync(command.RefreshToken,user.Id);

        if (refreshToken is null)
        {
            throw new RefreshTokenNotFoundException();
        }

        await _tokenRepository.RemoveRefreshToken(refreshToken);

    }
}