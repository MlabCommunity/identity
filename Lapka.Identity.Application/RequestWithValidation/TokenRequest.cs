using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Lapka.Identity.Application.RequestWithValidation;

public record TokenRequest(string Token);

public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();
    }
}