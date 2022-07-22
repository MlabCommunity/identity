namespace Lappka.Identity.Infrastructure.Storage;

internal interface IRequestStorage
{
    void Set<T>(string key, T value, TimeSpan? duration = null);
    T Get<T>(string key);
}