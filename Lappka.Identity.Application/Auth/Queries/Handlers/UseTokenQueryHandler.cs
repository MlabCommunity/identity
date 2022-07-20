using System.Security.Claims;
using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Entities;
using Lappka.Identity.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Lappka.Identity.Application.Auth.Queries.Handlers;

public class UseTokenQueryHandler : IQueryHandler<UseTokenQuery, TokensDto>
{
    private readonly AppUserManager _userManager;
    private readonly IJwtHandler _jwtHandler;
    private readonly ITokenRepository _tokenRepository;
    
    public UseTokenQueryHandler(AppUserManager userManager, ITokenRepository tokenRepository,
        IJwtHandler jwtHandler)
    {
        _tokenRepository = tokenRepository;
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<TokensDto> HandleAsync(UseTokenQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var decodedToken =_jwtHandler.DecodeToken(query.AccessToken);
        
        var user = await _userManager.FindByIdAsync(decodedToken.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier).Value);
        

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        var refreshToken = await _tokenRepository.GetRefreshTokenAsync(query.RefreshToken,user.Id);

        if (refreshToken is null)
        {
            throw new RefreshTokenNotFoundException();
        }

        if (!_jwtHandler.IsExpired(refreshToken.CreatedAt))
        {
            await _tokenRepository.RemoveRefreshToken(refreshToken);
            throw new RefreshTokenExpiredException();
        }

        return new TokensDto
        {
            AccessToken = _jwtHandler.CreateAccessToken(user.Id),
            RefreshToken = refreshToken.Value
        };
    }
    
}