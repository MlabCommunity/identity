using Lapka.Identity.Application.Commands;
using Lapka.Identity.Application.Queries;

namespace Lapka.Identity.Infrastructure.Services.Interfaces;

public interface IAuthService
{
    Task RegisterUser(UserRegCommand command);
    Task LoginUser(UserLogCommand command);
}

