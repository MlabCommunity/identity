using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Commands;

public record RegistrationCommand(string UserName,string Email, string Password, string ConfirmPassword) : ICommand;
