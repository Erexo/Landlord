namespace Infrastructure.Commands.Users
{
    public class ChangeUserPassword : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
