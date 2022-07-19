using Convey.CQRS.Queries;
using Lapka.Identity.Application.Interfaces;

namespace Lapka.Identity.Application.Queries.Handlers;

public class GetUserDataByIdQueryHandler : IQueryHandler<GetUserDataByIdQuery, GetUserDataQueryResult>
{
    private readonly IAppUserRepository _appUserRepository;

    public GetUserDataByIdQueryHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task<GetUserDataQueryResult> HandleAsync(GetUserDataByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _appUserRepository.GetFullUserById(query.UserId);

        var userData = new GetUserDataQueryResult(user.Id, user.UserName,
            user.UserExtended.FirstName, user.UserExtended.LastName,
            user.Email, user.UserExtended.CreatedAt);

        return userData;
    }
}

