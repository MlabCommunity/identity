using Lapka.Identity.Application.Commands;

namespace Lapka.Identity.Infrastructure.Services.Interfaces;

public interface IAuthService
{
    Task<Guid> Register(UserRegCommand userRegCommand);
    Task Login(UserLogCommand userLogDto);
}

