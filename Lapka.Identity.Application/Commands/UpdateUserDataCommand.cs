using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserDataCommand(string username, string firstName, string lastName) : ICommand;