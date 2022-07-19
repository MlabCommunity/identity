using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.JWT;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Auth.Queries.Handlers;

public class LoginQueryHandler : IQueryHandler<LoginQuery, TokensResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtHandler _jwtHandler;

    public LoginQueryHandler(SignInManager<ApplicationUser> signInManager, ITokenRepository tokenRepository,
        IJwtHandler jwtHandler)
    {
        _tokenRepository = tokenRepository;
        _signInManager = signInManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<TokensResponse> HandleAsync(LoginQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(query.Email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var result = await _signInManager.PasswordSignInAsync(user,
            query.Password, false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new UnableToLoginUser();
        }

        if (result.IsLockedOut)
        {
            throw new AccountIsLockedException();
        }

        var refreshToken = _jwtHandler.CreateRefreshToken();
        
        var appToken = new ApplicationToken()
        {
            LoginProvider = "Lappka",
            Name = Guid.NewGuid().ToString(),
            Value = refreshToken,
            UserId = user.Id
        };

        await _tokenRepository.AddRefreshToken(appToken);
        
        return new TokensResponse
        {
            AccessToken = _jwtHandler.CreateAccessToken(user.Id),
            RefreshToken = refreshToken
        };
    }
}