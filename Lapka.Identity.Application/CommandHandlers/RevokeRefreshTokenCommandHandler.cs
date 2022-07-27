using Convey.CQRS.Commands;
using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Exceptions.TokenExceptions;
using Lapka.Identity.Core.IRepository;

namespace Lapka.Identity.Application.CommandHandlers;

public class RevokeRefreshTokenCommandHandler : ICommandHandler<RevokeRefreshTokenCommand>
{
    private readonly IAppTokenRepository _appTokenRepository;

    public RevokeRefreshTokenCommandHandler(IAppTokenRepository appTokenRepository)
    {
        _appTokenRepository = appTokenRepository;
    }

    public async Task HandleAsync(RevokeRefreshTokenCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var token = await _appTokenRepository.GetRefreshToken(command.UserId, command.RefreshToken);
        if (token == null)
        {
            throw new InvalidRefreshTokenException();
        }

        await _appTokenRepository.RemoveRefreshToken(token);
    }
}

