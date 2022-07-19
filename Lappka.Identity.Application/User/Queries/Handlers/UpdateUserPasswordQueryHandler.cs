using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class UpdateUserPasswordQueryHandler : IQueryHandler<UpdateUserPasswordQuery, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserPasswordQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> HandleAsync(UpdateUserPasswordQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userManager.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
}