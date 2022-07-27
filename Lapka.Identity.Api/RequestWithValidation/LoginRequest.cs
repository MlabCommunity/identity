using FluentValidation;
using Lapka.Identity.Api.Helpers;

namespace Lapka.Identity.Api.RequestWithValidation;

public record LoginRequest(string Password, string Email);

internal class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Password)
            .MaximumLength(128)
            .MinimumLength(6)
            .Matches(ValidationRegexRules.PasswordRule);

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}