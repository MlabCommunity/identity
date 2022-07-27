using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UseRefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand
{
    public Guid TokenCacheId { get; } = Guid.NewGuid();
}

public record UseRefreshTokenResult(string AccessToken);
