using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserPasswordCommand(string Password) : ICommand;