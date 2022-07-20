namespace Lappka.Identity.Api.Requests;

public class ConfirmUpdateUserPasswordRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmedPassword { get; set; }
    public string ConfirmationToken { get; set; }
    
}