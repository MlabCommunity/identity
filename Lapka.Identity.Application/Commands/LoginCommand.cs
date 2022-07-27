using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record LoginCommand(string EmailAddress, string Password) : ICommand
{
    public Guid AccessTokenCacheId { get; } = Guid.NewGuid();
    public Guid RefreshTokenCacheId { get; } = Guid.NewGuid();
}

public record LoginResult(string AccessToken, string RefreshToken);