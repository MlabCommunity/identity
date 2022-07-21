using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Application.Services;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.Auth.Queries.Handlers;

public class GetTokensQueryHandler : IQueryHandler<GetTokensQuery, TokensDto>
{
    private readonly IJwtHandler _jwtHandler;
    private readonly IUserRepository _userRepository;

    public GetTokensQueryHandler(IJwtHandler jwtHandler, IUserRepository userRepository)
    {
        _jwtHandler = jwtHandler;
        _userRepository = userRepository;
    }

    public async Task<TokensDto> HandleAsync(GetTokensQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByEmailAsync(query.Email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return new TokensDto
        {
            AccessToken = _jwtHandler.CreateAccessToken(user.Id),
            RefreshToken = _jwtHandler.CreateRefreshToken()
        };
    }
}