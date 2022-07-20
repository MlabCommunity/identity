using FluentValidation;

namespace Lapka.Identity.Application.Validation.RequestWithValidation;

public record UserPasswordRequest(string Password);

public class UserPasswordRequestValidator : AbstractValidator<UserPasswordRequest>
{
    private const string rule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,128}$";
    public UserPasswordRequestValidator()
    {
        RuleFor(x => x.Password)
            .Matches(rule).WithMessage("Required at least 6 characters, one non alphanumeric character, one digit, one uppercase and one lowercase.");
    }
}