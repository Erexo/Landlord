using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class RemoveUserHandler : ICommandHandler<RemoveUser>
    {
        private IUserService _userRepository;

        public RemoveUserHandler(IUserService userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveUser command)
        {
            await _userRepository.RemoveAsync(command.Login, command.Password);
        }
    }
}
