using Core.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MongodbUserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public MongodbUserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetAsync(int id)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string login)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Login == login);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);


        public async Task RemoveAsync(User user)
            => await Users.DeleteOneAsync(x => x.Id == user.Id);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);
    }
}
