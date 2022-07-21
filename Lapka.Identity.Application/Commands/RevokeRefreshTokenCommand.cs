using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record RevokeRefreshTokenCommand(Guid UserId, string RefreshToken) : ICommand;