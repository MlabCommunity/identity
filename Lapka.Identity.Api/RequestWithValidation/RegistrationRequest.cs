using FluentValidation;
using Lapka.Identity.Api.Helpers;

namespace Lapka.Identity.Api.RequestWithValidation;

public record RegistrationRequest(string Username, string FirstName, string LastName, string EmailAddress, string Password, string ConfirmPassword);

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(user => user.Username)
            .MinimumLength(4)
            .MaximumLength(64)
            .NotEmpty()
            .Matches(ValidationRegexRules.NameRule)
            .WithMessage("Minimum 4 and maximum 64 characters. Allowed letters, numbers and characters: _ - .");

        RuleFor(user => user.EmailAddress)
            .EmailAddress()
            .NotEmpty();

        RuleFor(user => user.Password)
            .Matches(ValidationRegexRules.PasswordRule).WithMessage("Required at least 6 characters, one non alphanumeric character, one digit, one uppercase and one lowercase.")
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords must be equal.");

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(ValidationRegexRules.NameRule);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(ValidationRegexRules.NameRule);
    }
}