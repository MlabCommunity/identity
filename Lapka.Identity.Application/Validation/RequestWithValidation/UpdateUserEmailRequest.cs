using FluentValidation;

namespace Lapka.Identity.Application.Validation.RequestWithValidation;

public record UpdateUserEmailRequest(string email);

public class UpdateUserUserRequestValidator : AbstractValidator<UpdateUserEmailRequest>
{
    public UpdateUserUserRequestValidator()
    {
        RuleFor(x => x.email)
            .EmailAddress()
            .NotEmpty();
    }
}