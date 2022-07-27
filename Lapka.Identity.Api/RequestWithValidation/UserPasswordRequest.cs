using FluentValidation;
using Lapka.Identity.Api.Helpers;

namespace Lapka.Identity.Api.RequestWithValidation;

public record UserPasswordRequest(string Password);

internal class UserPasswordRequestValidator : AbstractValidator<UserPasswordRequest>
{
    public UserPasswordRequestValidator()
    {
        RuleFor(x => x.Password)
            .Matches(ValidationRegexRules.PasswordRule)
            .WithMessage("Required at least 6 characters, one non alphanumeric character, one digit, one uppercase and one lowercase.");
    }
}