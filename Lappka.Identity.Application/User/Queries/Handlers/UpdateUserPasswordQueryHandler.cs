using Convey.CQRS.Queries;
using Lappka.Identity.Application.Exceptions;
using Lappka.Identity.Core.Repositories;

namespace Lappka.Identity.Application.User.Queries.Handlers;

public class UpdateUserPasswordQueryHandler : IQueryHandler<UpdateUserPasswordQuery, string>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserPasswordQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> HandleAsync(UpdateUserPasswordQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return await _userRepository.GeneratePasswordResetTokenAsync(user);
    }
}