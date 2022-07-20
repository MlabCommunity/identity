namespace Lappka.Identity.Application.Exceptions;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
}