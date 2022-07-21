namespace Lappka.Identity.Infrastructure.Exceptions;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
}