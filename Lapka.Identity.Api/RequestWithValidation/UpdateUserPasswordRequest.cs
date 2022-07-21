using FluentValidation;
using Lapka.Identity.Api.Helpers;

namespace Lapka.Identity.Api.RequestWithValidation;

public record UpdateUserPasswordRequest(string Password, string ConfirmPassword);

internal class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    public UpdateUserPasswordRequestValidator()
    {
        RuleFor(user => user.Password)
            .Matches(ValidationRegexRules.PasswordRule)
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords must be equal.");
    }
}