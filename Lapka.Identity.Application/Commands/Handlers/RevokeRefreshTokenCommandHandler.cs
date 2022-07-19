using System.IdentityModel.Tokens.Jwt;
using Convey.CQRS.Commands;
using Lapka.Identity.Application.Interfaces;

namespace Lapka.Identity.Application.Commands.Handlers;

public class RevokeRefreshTokenCommandHandler : ICommandHandler<RevokeRefreshTokenCommand>
{
    private readonly IAppTokenRepository _appTokenRepository;
    private readonly IUserInfoProvider _userInfoProvider;

    public RevokeRefreshTokenCommandHandler(IAppTokenRepository appTokenRepository, IUserInfoProvider userInfoProvider)
    {
        _appTokenRepository = appTokenRepository;
        _userInfoProvider = userInfoProvider;
    }

    public async Task HandleAsync(RevokeRefreshTokenCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var id = _userInfoProvider.Id;

        if(id is null)
        {
            throw new Exception("Incorrect data");
        }

        await _appTokenRepository.RemoveRefreshTokenByData((Guid)id, command.RefreshToken);
    }
}

