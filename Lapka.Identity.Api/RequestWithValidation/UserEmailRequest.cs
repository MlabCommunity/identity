using FluentValidation;

namespace Lapka.Identity.Api.RequestWithValidation;

public record UserEmailRequest(string Email);

internal class UserEmailRequestValidator : AbstractValidator<UserEmailRequest>
{
    public UserEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}