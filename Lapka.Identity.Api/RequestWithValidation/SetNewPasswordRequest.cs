using FluentValidation;
using Lapka.Identity.Api.Helpers;

namespace Lapka.Identity.Api.RequestWithValidation;

public record SetNewPasswordRequest(string Password, string ConfirmPassword, string Email);

internal class SetNewPasswordRequestValidator : AbstractValidator<SetNewPasswordRequest>
{
    public SetNewPasswordRequestValidator()
    {
        RuleFor(x => x.Password)
            .MaximumLength(128)
            .MinimumLength(6)
            .Matches(ValidationRegexRules.PasswordRule)
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords must be equal.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}