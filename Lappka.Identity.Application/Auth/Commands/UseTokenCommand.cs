using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;

namespace Lappka.Identity.Application.Auth.Queries;

public record UseTokenCommand : ICommand
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public Guid AccessTokenCacheId { get; init; } = Guid.NewGuid();
    public Guid RefreshTokenCacheId { get; init; } = Guid.NewGuid();
}