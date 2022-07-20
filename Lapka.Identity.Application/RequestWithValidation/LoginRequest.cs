using FluentValidation;

namespace Lapka.Identity.Application.Validation.RequestWithValidation;

public record LoginRequest(string Password, string Email);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private const string rule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,128}$";
    public LoginRequestValidator()
    {
        RuleFor(x => x.Password)
            .MaximumLength(128)
            .MinimumLength(6)
            .Matches(rule);

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}