using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public record RegistrationCommand : ICommand
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
