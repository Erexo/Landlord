using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Core.Domain
{
    public class User
    {
        public int ID { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        [InverseProperty("Owner")]
        public IList<House> Houses { get; protected set; }
        
        [InverseProperty("Occupant")]
        public House Location { get; protected set; }


        private readonly Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        protected User()
        {
        }

        public User(string login, string password, string hash, string salt, string email)
        {
            SetLogin(login);
            SetPassword(password, hash);
            Salt = salt;
            SetEmail(email);
            CreationDate = DateTime.UtcNow;
        }

        private void SetLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new Exception("Empty Login.");
            if (login.Length < 4 || login.Length > 16)
                throw new Exception("Login lenght must be between 4 and 16");
            if (Login == login)
                return;

            Login = login;
            LastUpdate = DateTime.UtcNow;
        }

        public void SetPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Empty Password.");
            if (password.Length < 6 || password.Length > 32)
                throw new Exception("Password lenght must be between 6 and 32");
            if (Password == password)
                return;

            Password = hash;
            LastUpdate = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Empty email.");
            if (!emailRegex.IsMatch(email))
                throw new Exception("Invalid email format.");
            if (Email == email)
                return;

            Email = email;
            LastUpdate = DateTime.UtcNow;
        }
    }
}
