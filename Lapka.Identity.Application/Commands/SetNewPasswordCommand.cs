using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;
public record SetNewPasswordCommand(string Token, string Password, string Email) : ICommand;