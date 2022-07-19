using Convey.CQRS.Queries;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lapka.Identity.Application.Queries.Handlers;
internal class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, GetUserDataQueryResult>
{
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly SignInManager<AppUser> _signInManager;

    public GetUserDataQueryHandler(IUserInfoProvider userInfoProvider, SignInManager<AppUser> signInManager)
    {
        _userInfoProvider = userInfoProvider;
        _signInManager = signInManager;
    }

    public async Task<GetUserDataQueryResult> HandleAsync(GetUserDataQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userInfoProvider.GetCurrentUser();
        var userData = new GetUserDataQueryResult(user.Id, user.UserName,
            user.UserExtended.FirstName, user.UserExtended.LastName,
            user.Email, user.UserExtended.CreatedAt);

        return userData;
    }
}

