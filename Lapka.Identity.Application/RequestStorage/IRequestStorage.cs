namespace Lapka.Identity.Application.RequestStorage;


internal interface IRequestStorage
{
    void Set<T>(string key, T value, TimeSpan? duration = null);
    T Get<T>(string key);
}
