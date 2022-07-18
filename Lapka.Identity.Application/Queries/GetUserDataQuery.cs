using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Queries;

public record GetUserDataQuery() : IQuery<GetUserDataQueryResult>;

public record GetUserDataQueryResult(Guid Id, string username, string firstName, string lastName, string email, DateTime createdAt);