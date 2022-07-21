using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record LoginCommand(string EmailAddress, string Password) : ICommand
{
    public Guid AccTokenCasheId { get; init; } = Guid.NewGuid();
    public Guid RefTokenCasheId { get; init; } = Guid.NewGuid();
}

public record LoginResult(string AccessToken, string RefreshToken);