using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserDataCommand(Guid Id, string Username, string FirstName, string LastName) : ICommand;