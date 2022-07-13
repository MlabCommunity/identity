using Lapka.Identity.Application.DTO;

namespace Lapka.Identity.Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<string> Register(UserRegDto userRegDto);
    Task Login(UserLogDto userLogDto);
}

