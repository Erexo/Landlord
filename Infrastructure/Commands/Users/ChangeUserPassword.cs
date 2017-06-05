namespace Infrastructure.Commands.Users
{
    public class ChangeUserPassword : AuthenticatedCommand
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
