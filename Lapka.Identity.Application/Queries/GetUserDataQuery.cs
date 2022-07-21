using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Queries;

public record GetUserDataQuery(Guid Id) : IQuery<GetUserDataQueryResult>;

public record GetUserDataQueryResult(Guid Id, string Username, string FirstName, string LastName, string Email, DateTime CreatedAt);