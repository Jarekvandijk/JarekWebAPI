//using JarekWebAPI.WebApi;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace JarekWebAPI.Repositories
//{
//    public interface IAccountUserRepository
//    {
//        Task<AccountUser> InsertAsync(AccountUser userAccount);
//        Task<AccountUser?> ReadAsync(string username);
//        Task<IEnumerable<AccountUser>> ReadAsync();
//        Task UpdateAsync(AccountUser userAccount);
//        Task DeleteAsync(Guid id);
//    }
//}




namespace JarekWebAPI.WebApi.Repository
{
    public interface IAccountUserRepository
    {
        Task DeleteAsync(Guid id);
        Task<AccountUser> InsertAsync(AccountUser accountUser);
        Task<IEnumerable<AccountUser>> ReadAsync();
        Task<AccountUser?> ReadAsync(string username);    
        Task<AccountUser?> ReadAsync(Guid id);
        Task UpdateAsync(AccountUser accountUser);
    }
}