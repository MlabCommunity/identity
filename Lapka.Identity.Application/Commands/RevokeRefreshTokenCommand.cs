using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record RevokeRefreshTokenCommand(string RefreshToken) : ICommand;