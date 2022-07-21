using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UseRefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand
{
    public Guid TokenCasheId { get; init; } = Guid.NewGuid();
}

public record UseRefreshTokenResult(string AccessToken);
