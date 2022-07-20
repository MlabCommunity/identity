using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record ResetPasswordCommand(string Email) : ICommand;