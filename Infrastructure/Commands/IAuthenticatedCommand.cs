namespace Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        string Login { get; set; }
    }
}
