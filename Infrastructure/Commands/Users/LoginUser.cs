using System;

namespace Infrastructure.Commands.Users
{
    public class LoginUser : ICommand
    {
        public Guid TokenId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
