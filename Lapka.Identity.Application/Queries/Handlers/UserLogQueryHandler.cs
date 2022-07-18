using System.Security.Authentication;
using Convey.CQRS.Queries;
using Lapka.Identity.Application.Interfaces;
using Lapka.Identity.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Lapka.Identity.Application.Queries.Handlers;

internal class UserLogQueryHandler : IQueryHandler<UserLogQuery, UserLogResult>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public UserLogQueryHandler(IConfiguration config, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
    {
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<UserLogResult> HandleAsync(UserLogQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(query.EmailAddress);
        if (user is null)
        {
            throw new AuthenticationException("User doesn't exist.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);
        if (!result.Succeeded)
        {
            throw new AuthenticationException("Invalid password.");
        }

        var token = await _jwtGenerator.GenerateToken(user);

        return new UserLogResult(token);
    }
}

