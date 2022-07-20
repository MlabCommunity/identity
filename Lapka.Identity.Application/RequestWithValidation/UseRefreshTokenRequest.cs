﻿using FluentValidation;

namespace Lapka.Identity.Application.RequestWithValidation;

public record UseRefreshTokenRequest(string AccessToken, string RefreshToken);

public class UseRefreshTokenRequestValidator : AbstractValidator<UseRefreshTokenRequest>
{
    public UseRefreshTokenRequestValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}