﻿using System;
using System.Text.RegularExpressions;

namespace Core.Domain
{
    public class User
    {
        public Guid ID { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        private readonly Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        protected User()
        {
        }

        public User(string login, string password, string salt, string email)
        {
            ID = Guid.NewGuid();
            SetLogin(login);
            SetPassword(password);
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

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Empty Password.");
            if (password.Length < 6 || password.Length > 32)
                throw new Exception("Password lenght must be between 6 and 32");
            if (Password == password)
                return;

            Password = password;
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
