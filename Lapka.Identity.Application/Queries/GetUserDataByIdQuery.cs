using Convey.CQRS.Queries;

namespace Lapka.Identity.Application.Queries;

public record GetUserDataByIdQuery(Guid Id) : IQuery<GetUserDataQueryResult>;