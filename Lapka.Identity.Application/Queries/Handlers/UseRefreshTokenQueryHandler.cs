using Convey.CQRS.Queries;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Lapka.Identity.Application.Queries.Handlers;

internal class UseRefreshTokenQueryHandler : IQueryHandler<UseRefreshTokenQuery, UseRefreshTokenResult>
{
    private readonly IConfiguration _config;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public UseRefreshTokenQueryHandler(IConfiguration config, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
    {
        _config = config;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public Task<UseRefreshTokenResult> HandleAsync(UseRefreshTokenQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        // check refreshtoken in db
        //get user from accessToken

        //var accessToken = _jwtGenerator.GenerateAccessToken(user)
        // return accessToken

        
        throw new NotImplementedException();
    }
}
