namespace Lapka.Identity.Api.Helpers;

public interface IUserInfoProvider
{
    Guid Id { get; }
    string Name { get; }
    string Email { get; }
}