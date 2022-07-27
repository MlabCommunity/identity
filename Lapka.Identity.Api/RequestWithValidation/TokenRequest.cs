using FluentValidation;

namespace Lapka.Identity.Api.RequestWithValidation;

public record TokenRequest(string Token);

internal class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();
    }
}