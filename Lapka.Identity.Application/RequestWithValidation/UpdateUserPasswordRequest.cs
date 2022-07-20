using FluentValidation;

namespace Lapka.Identity.Application.RequestWithValidation;

public record UpdateUserPasswordRequest(string Password, string ConfirmPassword);

public class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    private const string passwordRule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,128}$";
    public UpdateUserPasswordRequestValidator()
    {
        RuleFor(user => user.Password)
            .Matches(passwordRule)
            .Equal(x => x.ConfirmPassword).WithMessage("Passwords must be equal.");
    }
}