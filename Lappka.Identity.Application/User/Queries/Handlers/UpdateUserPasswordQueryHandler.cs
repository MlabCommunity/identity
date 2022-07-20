using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions.Res;
using Lappka.Identity.Application.Services;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class UpdateUserPasswordQueryHandler : IQueryHandler<UpdateUserPasswordQuery, string>
{
    private readonly AppUserManager _appUserManager;

    public UpdateUserPasswordQueryHandler(AppUserManager appUserManager)
    {
        _appUserManager = appUserManager;
    }

    public async Task<string> HandleAsync(UpdateUserPasswordQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _appUserManager.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return await _appUserManager.GeneratePasswordResetTokenAsync(user);
    }
}