using FluentValidation;
using Lapka.Identity.Api.Helpers;

namespace Lapka.Identity.Api.RequestWithValidation;

public record UpdateUserDataRequest(string Username, string FirstName, string LastName);

internal class UpdateUserDataRequestValidator : AbstractValidator<UpdateUserDataRequest>
{
    public UpdateUserDataRequestValidator()
    {
        RuleFor(user => user.Username)
            .MinimumLength(4)
            .MaximumLength(64)
            .Matches(ValidationRegexRules.NameRule)
            .WithMessage("Minimum 4 and maximum 64 characters. Allowed letters, numbers and characters: _ - .");

        RuleFor(user => user.FirstName)
            .MaximumLength(32)
            .Matches(ValidationRegexRules.NameRule)
            .WithMessage("Minimum 2 and maximum 32 characters. Allowed letters, numbers and characters: _ - .");

        RuleFor(user => user.LastName)
            .MaximumLength(32).Matches(ValidationRegexRules.NameRule)
            .WithMessage("Minimum 2 and maximum 32 characters. Allowed letters, numbers and characters: _ - .");
    }
}