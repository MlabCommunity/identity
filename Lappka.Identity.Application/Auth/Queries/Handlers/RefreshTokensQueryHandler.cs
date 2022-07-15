using Convey.CQRS.Queries;
using Lappka.Identity.Application.JWT;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Queries.Handlers;

public class RefreshTokensQueryHandler : IQueryHandler<RefreshTokensQuery, JwtResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtHandler _jwtHandler;

    public RefreshTokensQueryHandler(UserManager<ApplicationUser> userManager,
        IJwtHandler jwtHandler)
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<JwtResponse> HandleAsync(RefreshTokensQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {

        return null;
    }
}