namespace Infrastructure.Commands
{
    public class AuthenticatedCommand : IAuthenticatedCommand
    {
        public string Login { get; set; }
    }
}
