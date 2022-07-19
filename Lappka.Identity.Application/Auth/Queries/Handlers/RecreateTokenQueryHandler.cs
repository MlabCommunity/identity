using Convey.CQRS.Queries;
using Lappka.Identity.Application.Auth.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.JWT;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Queries.Handlers;

public class RecreateTokenQueryHandler : IQueryHandler<RecreateTokenQuery, JwtResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtHandler _jwtHandler;
    
    public RecreateTokenQueryHandler(UserManager<ApplicationUser> userManager,
        IJwtHandler jwtHandler)
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<JwtResponse> HandleAsync(RecreateTokenQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        
        return null;
    }
}