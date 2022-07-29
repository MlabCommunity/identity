using FluentValidation;

namespace Lappka.Identity.Api.Requests.Validations;

public class UpdateUserEmailRequestValidator : AbstractValidator<UpdateEmailRequest>
{
    public UpdateUserEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}