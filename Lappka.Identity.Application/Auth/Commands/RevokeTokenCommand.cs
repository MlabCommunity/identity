using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public class RevokeTokenCommand : ICommand
{
    public string UserId;
    public string RefreshToken { get; set; }
}