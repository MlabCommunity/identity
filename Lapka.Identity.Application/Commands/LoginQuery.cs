using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Commands;

public record LoginQuery(string EmailAddress, string Password) : IQuery<LoginResult>;

public record LoginResult(string AccessToken, string RefreshToken);