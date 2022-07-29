namespace Lappka.Identity.Api.Requests.Validations.Consts;

public static class AppRegexs
{
    public static string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]*$";
    public static string NameRegex = @"^[a-zA-Z0-9_]*$";
    public static string UserNameRegex = @"^[a-zA-Z0-9_\-\.]+$";
}