using FluentValidation;
using Lapka.Identity.Application.Commands;

namespace Lapka.Identity.Application.Validation;

public class UserRegCommandValidator : AbstractValidator<RegistrationCommand>
{
    private const string usernameRule = @"^[a-zA-Z0-9_\-\.]+$";
    private const string passwordRule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    public UserRegCommandValidator()
    {
        RuleFor(user => user.UserName).MinimumLength(4).MaximumLength(64).NotEmpty()
            .Matches(usernameRule).WithMessage("Allowed letters, numbers and characters: _ - .");
        RuleFor(user => user.EmailAddress).EmailAddress().NotEmpty();
        RuleFor(user => user.Password).MaximumLength(128).MinimumLength(6).Matches(passwordRule);
    }
}