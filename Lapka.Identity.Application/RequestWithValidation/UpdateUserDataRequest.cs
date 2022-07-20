using FluentValidation;

namespace Lapka.Identity.Application.RequestWithValidation;

public record UpdateUserDataRequest(string Username, string FirstName, string LastName);

public class UpdateUserDataRequestValidator : AbstractValidator<UpdateUserDataRequest>
{
    private const string nameRule = @"^[a-zA-Z0-9_\-\.]{2,64}$";
    public UpdateUserDataRequestValidator()
    {
        RuleFor(user => user.Username)
            .MinimumLength(4)
            .MaximumLength(64)
            .Matches(nameRule)
            .WithMessage("Minimum 4 and maximum 64 characters. Allowed letters, numbers and characters: _ - .");

        RuleFor(user => user.FirstName)
            .MaximumLength(32)
            .Matches(nameRule)
            .WithMessage("Minimum 2 and maximum 32 characters. Allowed letters, numbers and characters: _ - .");

        RuleFor(user => user.LastName)
            .MaximumLength(32).Matches(nameRule)
            .WithMessage("Minimum 2 and maximum 32 characters. Allowed letters, numbers and characters: _ - .");
    }
}