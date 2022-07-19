namespace Lappka.Identity.Application.Dto;

public record UserDto
{
    public string Id { get; set; } 
    public string Email { get; set; } 
    public string Firstname { get; set; } 
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
}