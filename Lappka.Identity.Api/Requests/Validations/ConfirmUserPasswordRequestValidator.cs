using FluentValidation;
using static Lappka.Identity.Api.Requests.Validations.Consts.AppRegexs;

namespace Lappka.Identity.Api.Requests.Validations;

public class ConfirmUserPasswordRequestValidator : AbstractValidator<ConfirmUpdateUserPasswordRequest>
{
    
    public ConfirmUserPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x=>x.ConfirmedPassword)
            .Equal(x=>x.Password).WithMessage("Passwords do not match");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(30)
            .Matches(PasswordRegex);
    }
}