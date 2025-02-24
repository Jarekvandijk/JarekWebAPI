using Microsoft.Data.SqlClient;
using JarekWebAPI.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace JarekWebAPI.Repositories
{
    public class Object2DRepository
    {
        private readonly string sqlConnectionString;

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D> InsertAsync(Object2D obj)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var objectId = await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, PrefabId, PositionX, PositionY, ScaleX, ScaleY, SortingLayer) VALUES (@Id, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @SortingLayer)", obj);
                return obj;
            }
        }

        public async Task<Object2D?> ReadAsync(int id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>("SELECT * FROM [Object2D] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Object2D>> ReadAllAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D]");
            }
        }

        public async Task UpdateAsync(Object2D obj)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "UPDATE [Object2D] SET " +
                    "PrefabId = @PrefabId, " +
                    "PositionX = @PositionX, " +
                    "PositionY = @PositionY, " +
                    "ScaleX = @ScaleX, " +
                    "ScaleY = @ScaleY, " +
                    "SortingLayer = @SortingLayer " +
                    "WHERE Id = @Id", obj);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object2D] WHERE Id = @Id", new { id });
            }
        }
    }
}
