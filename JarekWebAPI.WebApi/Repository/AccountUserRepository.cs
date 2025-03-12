//using Microsoft.Data.SqlClient; 
//using Dapper;
//using JarekWebAPI.WebApi;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace JarekWebAPI.Repositories
//{
//    public class AccountUserRepository : IAccountUserRepository
//    {
//        private readonly string sqlConnectionString;

//        public AccountUserRepository(string sqlConnectionString)
//        {
//            this.sqlConnectionString = sqlConnectionString;
//        }

//        public async Task<AccountUser> InsertAsync(AccountUser userAccount)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                await sqlConnection.ExecuteAsync("INSERT INTO [AccountUser] (Id, UserName, Password) VALUES (@Id, @UserName, @Password)", userAccount);
//                return userAccount;
//            }
//        }

//        public async Task<AccountUser?> ReadAsync(string username)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                return await sqlConnection.QuerySingleOrDefaultAsync<AccountUser>("SELECT * FROM [AccountUser] WHERE UserName = @UserName", new { username });
//            }
//        }

//        public async Task<IEnumerable<AccountUser>> ReadAsync()
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                return await sqlConnection.QueryAsync<AccountUser>("SELECT * FROM [AccountUser]");
//            }
//        }

//        public async Task UpdateAsync(AccountUser userAccount)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                await sqlConnection.ExecuteAsync(
//                    "UPDATE [AccountUser] SET " +
//                    "UserName = @UserName, " +
//                    "Password = @Password " +
//                    "WHERE Id = @Id", userAccount);
//            }
//        }

//        public async Task DeleteAsync(Guid id)
//        {
//            using (var sqlConnection = new SqlConnection(sqlConnectionString))
//            {
//                await sqlConnection.ExecuteAsync("DELETE FROM [AccountUser] WHERE Id = @Id", new { id });
//            }
//        }
//    }
//}





using Dapper;
using Microsoft.Data.SqlClient;
using JarekWebAPI.WebApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JarekWebAPI.WebApi.Repository;

namespace JarekWebAPI.Repositories
{
    public class AccountUserRepository : IAccountUserRepository
    {
        private readonly string sqlConnectionString;

        public AccountUserRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }
        public async Task<AccountUser> InsertAsync(AccountUser accountUser)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "INSERT INTO [AccountUser] (Id, UserName, Password) VALUES (@Id, @UserName, @Password)";
                await sqlConnection.ExecuteAsync(query, accountUser);
                return accountUser;
            }
        }
        public async Task<AccountUser?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "SELECT * FROM [AccountUser] WHERE Id = @Id";
                return await sqlConnection.QuerySingleOrDefaultAsync<AccountUser>(query, new { id });
            }
        }
        public async Task<IEnumerable<AccountUser>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "SELECT * FROM [AccountUser]";
                return await sqlConnection.QueryAsync<AccountUser>(query);
            }
        }
        public async Task UpdateAsync(AccountUser accountUser)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "UPDATE [AccountUser] SET " +
                            "UserName = @UserName, " +
                            "Password = @Password " +
                            "WHERE Id = @Id";
                await sqlConnection.ExecuteAsync(query, accountUser);
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "DELETE FROM [AccountUser] WHERE Id = @Id";
                await sqlConnection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<AccountUser?> ReadAsync(string username)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<AccountUser>("SELECT * FROM [AccountUser] WHERE UserName = @UserName", new { username }); 
            }
        }

        public async Task<AccountUser?> CheckUserCredentialsAsync(string username, string password)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "SELECT * FROM [AccountUser] WHERE UserName = @UserName AND Password = @Password";
                return await sqlConnection.QuerySingleOrDefaultAsync<AccountUser>(query, new { username, password });
            }
        }
    }
}