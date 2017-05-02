namespace Infrastructure.Commands.Users
{
    public class RemoveUser : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
