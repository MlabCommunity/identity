namespace Lapka.Identity.Application.RequestStorage;

public interface IUserRequestStorage
{
    void SetToken(Guid commandId, string token);
    string GetToken(Guid commandId);
}