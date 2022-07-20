using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.Auth.Queries.Handlers;

public class LoginQueryHandler : IQueryHandler<LoginQuery, TokensDto>
{
    private readonly AppSignInManager _appSignInManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtHandler _jwtHandler;

    
    public LoginQueryHandler(AppSignInManager appSignInManager,UserManager<AppUser> appUserManager, ITokenRepository tokenRepository,
        IJwtHandler jwtHandler)
    {

        _tokenRepository = tokenRepository;
        _appSignInManager = appSignInManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<TokensDto> HandleAsync(LoginQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _appSignInManager.UserManager.FindByEmailAsync(query.Email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        var result = await _appSignInManager.CheckPasswordSignInAsync(user, query.Password, false);

        if (!result.Succeeded)
        {
            throw new UnableToLoginUser();
        }

        if (result.IsLockedOut)
        {
            throw new AccountIsLockedException();
        }

        var refreshToken = _jwtHandler.CreateRefreshToken();
        
        var appToken = new AppToken()
        {
            LoginProvider = "Lappka",
            Name = Guid.NewGuid().ToString(),
            Value = refreshToken,
            UserId = user.Id
        };

        await _tokenRepository.AddRefreshToken(appToken);
        
        return new TokensDto
        {
            AccessToken = _jwtHandler.CreateAccessToken(user.Id),
            RefreshToken = refreshToken
        };
    }
}