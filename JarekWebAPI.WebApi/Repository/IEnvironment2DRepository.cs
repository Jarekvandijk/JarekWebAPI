﻿using JarekWebAPI.WebApi;

namespace JarekWebAPI.Repositories
{
    public interface IEnvironment2DRepository
    {
        Task DeleteAsync(int id);
        Task<Environment2D> InsertAsync(Environment2D environment);
        Task<IEnumerable<Environment2D>> ReadAllAsync();
        Task<Environment2D?> ReadAsync(int id);
        Task UpdateAsync(Environment2D environment);
    }
}