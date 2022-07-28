using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public record UseTokenCommand : ICommand
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public Guid AccessTokenCacheId { get; init; } = Guid.NewGuid();
    public Guid RefreshTokenCacheId { get; init; } = Guid.NewGuid();
}