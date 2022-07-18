namespace Lapka.Identity.Core.IRepository
{
    public interface IUserExtendedRepository
    {
        Task CreateUserExtended(Guid id, string firstName, string lastName);
        Task UpdateModifiedTime(Guid id);
        Task DeleteUserExtended(Guid id);
    }
}
