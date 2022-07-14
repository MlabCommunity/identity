namespace Lapka.Identity.Core.IRepository
{
    public interface IUserExtendedRepository
    {
        Task CreateUserExtended(Guid id);
        Task UpdateModifiedTime(Guid id);
        Task DeleteUserExtended(Guid id);
    }
}
