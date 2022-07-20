using Convey.CQRS.Queries;
using Lappka.Identity.Application.Dto;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery,UserDto>
{
    
    private readonly AppUserManager _appUserManager;

    public GetUserByIdQueryHandler(AppUserManager appUserManager)
    {
        _appUserManager = appUserManager;
    }
    
    public async Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _appUserManager.FindByIdAsync(query.UserId);
        
        
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.UserExtended.FirstName,
            Lastname = user.UserExtended.LastName,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName
        };
    }
}