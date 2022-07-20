using FluentValidation;

namespace Lapka.Identity.Application.Validation.RequestWithValidation;

public record SetNewPasswordRequest(string Password, string ConfirmPassword, string Email);

public class SetNewPasswordRequestValidator : AbstractValidator<SetNewPasswordRequest>
{
    private const string rule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    public SetNewPasswordRequestValidator()
    {
        RuleFor(x => x.Password)
            .MaximumLength(128)
            .MinimumLength(6)
            .Matches(rule)
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords must be equal.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}