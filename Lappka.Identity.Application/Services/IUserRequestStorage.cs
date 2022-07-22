namespace Lappka.Identity.Application.Services;

public interface IUserRequestStorage
{
    void SetToken(Guid commandId, string token);
    string GetToken(Guid commandId);
}