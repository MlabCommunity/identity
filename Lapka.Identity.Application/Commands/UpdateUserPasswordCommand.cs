using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserPasswordCommand(Guid Id, string Password) : ICommand;