using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserEmailCommand(string email) : ICommand;