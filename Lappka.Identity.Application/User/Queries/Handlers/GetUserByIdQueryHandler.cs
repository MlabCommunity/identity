using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery,UserDto>
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    
    public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByIdAsync(query.UserId);
        
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName
        };
    }
}