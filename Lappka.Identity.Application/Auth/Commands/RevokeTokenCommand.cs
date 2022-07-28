using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public record RevokeTokenCommand : ICommand
{
    public Guid UserId { get; init; }
    public string RefreshToken { get; init; }
}