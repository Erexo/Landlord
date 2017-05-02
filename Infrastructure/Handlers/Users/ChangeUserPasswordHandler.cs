using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        private readonly IUserService _userService;

        public ChangeUserPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChangeUserPassword command)
        {
            var user = await _userService.GetAsync(command.Login);

            if (user == null)
                throw new Exception($"User '{command.Login}' does not exists.");

            //TODO: Nie ma jak pobrać hasła z DTO
        }
    }
}
