using Lappka.Identity.Application.Services;

namespace Lappka.Identity.Infrastructure.Storage;

internal sealed class UserRequestStorage : IUserRequestStorage
{
    private readonly IRequestStorage _requestStorage;

    public UserRequestStorage(IRequestStorage requestStorage)
    {
        _requestStorage = requestStorage;
    }

    private static string GetKey(Guid commandId) => $"token:{commandId:N}";

    public void SetToken(Guid commandId, string token)
    {
        _requestStorage.Set(GetKey(commandId), token);
    }

    public string GetToken(Guid commandId)
    {
        return _requestStorage.Get<string>(GetKey(commandId));
    }
}