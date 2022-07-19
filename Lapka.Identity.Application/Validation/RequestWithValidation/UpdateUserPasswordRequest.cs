using FluentValidation;

namespace Lapka.Identity.Application.Validation.RequestWithValidation;

public record UpdateUserPasswordRequest(string Password);

public class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    private const string rule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    public UpdateUserPasswordRequestValidator()
    {
        RuleFor(x => x.Password)
        .MaximumLength(128)
        .MinimumLength(6)
        .Matches(rule);
    }
}