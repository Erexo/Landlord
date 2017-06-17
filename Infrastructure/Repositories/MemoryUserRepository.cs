using Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MemoryUserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();


        public async Task<User> GetAsync(int id)
            => await Task.FromResult(_users.SingleOrDefault(o => o.Id == id));

        public async Task<User> GetAsync(string login)
            => await Task.FromResult(_users.SingleOrDefault(o => o.Login == login));

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            //throw new NotImplementedException();
            await Task.CompletedTask;
        }
    }
}
