using System.ComponentModel.DataAnnotations;
using Convey.CQRS.Commands;

namespace Lappka.Identity.Application.Auth.Commands;

public record RegistrationCommand : ICommand
{
    [Required]
    public string Username { get; set; }
    [EmailAddress,Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}
