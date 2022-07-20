using FluentValidation;

namespace Lapka.Identity.Application.Validation.RequestWithValidation;

public record UserEmailRequest(string Email);

public class UserEmailRequestValidator : AbstractValidator<UserEmailRequest>
{
    public UserEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}