using FluentValidation;
using static Lappka.Identity.Api.Requests.Validations.Consts.AppRegexs;

namespace Lappka.Identity.Api.Requests.Validations;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .Matches(NameRegex);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .Matches(NameRegex);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .Matches(NameRegex);
        
        RuleFor(x=>x.PhoneNumber)
            .NotEmpty()
            .Matches(PhoneNumberRegex);
    }
}