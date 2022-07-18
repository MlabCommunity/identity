using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Queries;

public record UserLogQuery(string EmailAddress, string Password) : IQuery<UserLogResult>;

public record UserLogResult(string AccessToken, string RefreshToken);