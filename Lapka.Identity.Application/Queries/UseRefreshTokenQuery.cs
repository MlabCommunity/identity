using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Queries;

public record UseRefreshTokenQuery(string AccessToken, string RefreshToken) : IQuery<UseRefreshTokenResult>;
public record UseRefreshTokenResult(string AccessToken);
