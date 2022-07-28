using Convey.CQRS.Commands;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.Auth.Commands.Handlers;

public class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtHandler _jwtHandler;
    private readonly ITokenRepository _tokenRepository;

    public RevokeTokenCommandHandler(IUserRepository userRepository, IJwtHandler jwtHandler,
        ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _jwtHandler = jwtHandler;
        _tokenRepository = tokenRepository;
    }

    public async Task HandleAsync(RevokeTokenCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(command.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var refreshToken = await _tokenRepository.GetRefreshTokenAsync(command.RefreshToken, user.Id);

        if (refreshToken is null)
        {
            throw new RefreshTokenNotFoundException();
        }

        await _tokenRepository.RemoveRefreshTokenAsync(refreshToken);
    }
}