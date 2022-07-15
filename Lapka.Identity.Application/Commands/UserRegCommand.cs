using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record UserRegCommand(string UserName, string EmailAddress, string Password) : ICommand;