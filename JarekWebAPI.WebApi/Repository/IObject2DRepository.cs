using JarekWebAPI.WebApi;

namespace JarekWebAPI.Repositories
{
    public interface IObject2DRepository
    {
        Task DeleteAsync(int id);
        Task<Object2D> InsertAsync(Object2D obj);
        Task<IEnumerable<Object2D>> ReadAllAsync();
        Task<Object2D?> ReadAsync(int id);
        Task UpdateAsync(Object2D obj);
    }
}