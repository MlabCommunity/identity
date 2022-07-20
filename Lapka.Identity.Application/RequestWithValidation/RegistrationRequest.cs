using FluentValidation;

namespace Lapka.Identity.Application.Commands;

public record RegistrationRequest(string Username, string FirstName, string LastName, string EmailAddress, string Password, string ConfirmPassword);

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    private const string nameRule = @"^[a-zA-Z0-9_\-\.]{2,64}$";
    private const string passwordRule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,128}$";
    public RegistrationRequestValidator()
    {
        RuleFor(user => user.Username)
            .MinimumLength(4)
            .MaximumLength(64)
            .NotEmpty()
            .Matches(nameRule)
            .WithMessage("Minimum 4 and maximum 64 characters. Allowed letters, numbers and characters: _ - .");

        RuleFor(user => user.EmailAddress)
            .EmailAddress()
            .NotEmpty();

        RuleFor(user => user.Password)
            .Matches(passwordRule).WithMessage("Required at least 6 characters, one non alphanumeric character, one digit, one uppercase and one lowercase.")
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords must be equal.");

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(nameRule);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .MaximumLength(32)
            .Matches(nameRule);
    }
}