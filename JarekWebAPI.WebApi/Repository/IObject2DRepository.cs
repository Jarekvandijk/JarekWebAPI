namespace JarekWebAPI.WebApi.Repository
{
    public interface IObject2DRepository
    {
        Task DeleteAsync(Guid id);
        Task<Object2D> InsertAsync(Object2D obj);
        Task<IEnumerable<Object2D>> ReadAllAsync();
        Task<Object2D?> ReadAsync(Guid id);
        Task UpdateAsync(Object2D obj);
    }
}