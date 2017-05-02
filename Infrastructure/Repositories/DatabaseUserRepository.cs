using Core.Domain;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DatabaseUserRepository : IUserRepository
    {
        private LandlordContext _databaseContext;

        public DatabaseUserRepository(LandlordContext databaseContext)
        {
            _databaseContext = databaseContext;
            _databaseContext.Database.EnsureCreated();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_databaseContext.Users.ToList());


        public async Task<User> GetAsync(int id)
            => await _databaseContext.Users.FindAsync(id);

        public async Task<User> GetAsync(string login)
            => await Task.FromResult(_databaseContext.Users.FirstOrDefault(o => o.Login == login));

        public async Task AddAsync(User user)
        {
            await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _databaseContext.Users.Remove(user);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
