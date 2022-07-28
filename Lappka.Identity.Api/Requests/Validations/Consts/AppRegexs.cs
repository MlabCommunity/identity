namespace Lappka.Identity.Api.Requests.Validations.Consts;

public static class AppRegexs
{
    public static string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]*$";
    public static string NameRegex = @"^[a-zA-Z0-9_]*$";
    public static string UserNameRegex = @"^[a-zA-Z0-9_\-\.]+$";
    public static string PhoneNumberRegex = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
}