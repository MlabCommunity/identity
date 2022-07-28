using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public record RegistrationCommand : ICommand
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}