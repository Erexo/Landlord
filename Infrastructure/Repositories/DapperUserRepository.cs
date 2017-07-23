using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Dapper;
using System.Data;

namespace Infrastructure.Repositories
{
    public class DapperUserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public DapperUserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        { 
            return null;
        }

        public async Task<User> GetAsync(int id)
        {
            using (var connection = _dbConnection)
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE ID = @ID", new { ID = id });
                return user;
            }
        }

        public async Task<User> GetAsync(string login)
        {
            using (var connection = _dbConnection)
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE login = @Login", new { Login = login });
                return user;
            }
        }

        public async Task AddAsync(User user)
        {
            using (var connection = _dbConnection)
            {
                connection.Open();

            }
        }

        public async Task UpdateAsync(User user)
        {

        }

        public async Task RemoveAsync(User user)
        {

        }
    }
}
