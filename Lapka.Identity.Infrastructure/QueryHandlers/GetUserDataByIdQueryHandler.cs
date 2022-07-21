using Convey.CQRS.Queries;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Queries;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.QueryHandlers;

public class GetUserDataByIdQueryHandler : IQueryHandler<GetUserDataByIdQuery, GetUserDataQueryResult>
{
    private readonly DataContext _context;

    public GetUserDataByIdQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<GetUserDataQueryResult> HandleAsync(GetUserDataByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _context.Users
            .Include(x => x.UserExtended)
            .FirstOrDefaultAsync(x => x.Id == query.Id);

        if (user is null || user.UserExtended is null)
        {
            throw new UserNotFoundException();
        }

        var userData = new GetUserDataQueryResult(user.Id, user.UserName,
            user.UserExtended.FirstName, user.UserExtended.LastName,
            user.Email, user.UserExtended.CreatedAt);

        return userData;
    }
}

