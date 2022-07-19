using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Queries;

public record LoginQuery(string EmailAddress, string Password) : IQuery<LoginResult>;

public record LoginResult(string AccessToken, string RefreshToken);