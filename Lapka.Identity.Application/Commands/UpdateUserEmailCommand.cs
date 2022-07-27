using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserEmailCommand(Guid Id, string Email) : ICommand;