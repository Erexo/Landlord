using Core.Domain;
using Infrastructure.DTO;
using Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
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
                LastUpdate = user.LastUpdate,
                Houses = user.Houses,
                Location = user.Location
            };
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            List<UserDTO> DTOusers = new List<UserDTO>();

            foreach(User user in users)
            {
                UserDTO DTOuser = new UserDTO
                {
                    ID = user.ID,
                    Login = user.Login,
                    FullName = user.FullName,
                    Email = user.Email,
                    CreationDate = user.CreationDate,
                    LastUpdate = user.LastUpdate,
                    Houses = user.Houses,
                    Location = user.Location
                };

                DTOusers.Add(DTOuser);
            }

            return DTOusers;
        }

        public async Task RegisterAsync(string login, string password, string email)
        {
            User user = await _userRepository.GetAsync(login);

            if (user != null)
                throw new Exception($"User '{login}' already exists.");

            var users = await _userRepository.GetAllAsync();
            if (users.SingleOrDefault(o => o.Email == email) != null)
                throw new Exception($"Email '{email}' is already registered.");

            string salt = _encrypter.GetSalt(password);
            string hash = _encrypter.GetHash(password, salt);

            user = new User(login, password, hash, salt, email);
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

        public async Task LoginAsync(string login, string password)
        {
            User user = await _userRepository.GetAsync(login);

            if (user == null)
                throw new Exception($"Invalid user data.");
            
            string hash = _encrypter.GetHash(password, user.Salt);

            if (user.Password != hash)
                throw new Exception($"Invalid user data.");

        }
    }
}
