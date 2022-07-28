using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public record LoginCommand : ICommand
{
    public string Email { get; init; }
    public string Password { get; init; }
    public Guid AccessTokenCacheId { get; init; } = Guid.NewGuid();
    public Guid RefreshTokenCacheId { get; init; } = Guid.NewGuid();
}