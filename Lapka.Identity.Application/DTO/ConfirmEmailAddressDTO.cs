namespace Lapka.Identity.Application.DTO;

public record ConfirmEmailAddressDTO(string Email, string Token, string Username, string FirstName, string LastName, Guid UserId);
