using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UpdateUserDataCommand(string Username, string FirstName, string LastName) : ICommand;