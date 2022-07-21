using FluentValidation;

namespace Lapka.Identity.Api.RequestWithValidation;

public record UseRefreshTokenRequest(string AccessToken, string RefreshToken);

internal class UseRefreshTokenRequestValidator : AbstractValidator<UseRefreshTokenRequest>
{
    public UseRefreshTokenRequestValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}