namespace JarekWebAPI.WebApi.Repository
{
    public interface IAccountUserRepository
    {
            Task DeleteAsync(Guid id);
            Task<AccountUser> InsertAsync(AccountUser accountUser);
            Task<IEnumerable<AccountUser>> ReadAsync();
            Task<AccountUser?> ReadAsync(Guid id);
            Task UpdateAsync(AccountUser accountUser);
    }
}
