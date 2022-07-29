using FluentValidation;

namespace Lapka.Identity.Api.RequestWithValidation;

public record UpdateUserEmailRequest(string Email);

public class UpdateUserEmailRequestValidator : AbstractValidator<UpdateUserEmailRequest>
{
    public UpdateUserEmailRequestValidator()
    {
        RuleFor(user => user.Email)
            .EmailAddress()
            .NotEmpty();
    }
}