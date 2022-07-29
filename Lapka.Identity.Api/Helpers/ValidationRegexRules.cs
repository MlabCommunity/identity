namespace Lapka.Identity.Api.Helpers;

internal static class ValidationRegexRules
{
    public const string NameRule = @"^[a-zA-ZzżźćńółęąśŻŹĆĄŚĘŁÓŃ0-9_\-\.]{2,64}$";
    public const string PasswordRule = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,128}$";
}

