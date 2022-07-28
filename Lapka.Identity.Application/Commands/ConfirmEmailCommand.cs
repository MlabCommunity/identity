using Convey.CQRS.Commands;

namespace Lapka.Identity.Application.Commands;

public record ConfirmEmailCommand(string ConfirmEmailToken) : ICommand;