using Convey.CQRS.Queries;
using Lapka.Identity.Application.Exceptions.UserExceptions;
using Lapka.Identity.Application.Queries;
using Lapka.Identity.Core.IRepository;
using Lapka.Identity.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Identity.Infrastructure.QueryHandlers;

internal class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, GetUserDataQueryResult>
{
    private readonly DataContext _context;

    public GetUserDataQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<GetUserDataQueryResult> HandleAsync(GetUserDataQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        if(query.Id == Guid.Empty)
            throw new UserNotFoundException();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == query.Id);
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

