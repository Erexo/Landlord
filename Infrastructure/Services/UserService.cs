using Core.Domain;
using Infrastructure.DTO;
using Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetAsync(string login)
        {
            User user = await _userRepository.GetAsync(login);

            if (user == null)
            {
                //throw new Exception($"User with login '{login}' does not exists.");
                return null;
            }

            return new UserDTO
            {
                ID = user.ID,
                Login = user.Login,
                FullName = user.FullName,
                Email = user.Email,
                CreationDate = user.CreationDate,
                LastUpdate = user.LastUpdate
            };
        }

        public async Task RegisterAsync(string login, string password, string email)
        {
            User user = await _userRepository.GetAsync(login);

            if (user != null)
                throw new Exception($"User '{login}' already exists.");

            var users = await _userRepository.GetAllAsync();
            if (users.SingleOrDefault(o => o.Email == email) != null)
                throw new Exception($"Email '{email}' is already registered.");
            
            string salt = Guid.NewGuid().ToString("N");
            user = new User(login, password, salt, email);
            await _userRepository.AddAsync(user);
        }

        public async Task RemoveAsync(string login, string password)
        {
            User user = await _userRepository.GetAsync(login);

            if (user == null)
                throw new Exception($"User with login '{login}' does not exist.");

            if (user.Password != password)
                throw new Exception($"User password does not match with given password.");

            await _userRepository.RemoveAsync(user);
        }
    }
}
