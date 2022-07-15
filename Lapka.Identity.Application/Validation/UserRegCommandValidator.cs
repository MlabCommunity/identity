using FluentValidation;
using Lapka.Identity.Application.Commands;

namespace Lapka.Identity.Application.Validation;

public class UserRegCommandValidator : AbstractValidator<UserRegCommand>
{
    private const string rule = @"^[a-zA-Z0-9_\-\.]+$";
    public UserRegCommandValidator()
    {
        RuleFor(user => user.UserName).MinimumLength(4).MaximumLength(64).NotEmpty()
            .Matches(rule).WithMessage("Allowed letters, numbers and characters: _ - .");
        RuleFor(user => user.EmailAddress).EmailAddress().NotEmpty();
        RuleFor(user => user.Password).MaximumLength(128).MinimumLength(6).NotEmpty();
    }
}