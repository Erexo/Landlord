using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using Infrastructure.Settings;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;
        private readonly UserSettings _userSettings;
        public CreateUserHandler(IUserService userService, UserSettings userSettings)
        {
            _userService = userService;
            _userSettings = userSettings;
        }

        public async Task HandleAsync(CreateUser command)
        {
            var users = await _userService.GetAllAsync();
            if(_userSettings.MaxUsers <= users.Count)
            {
                throw new Exception("Cannot register new user. Too many users.");
            }

            await _userService.RegisterAsync(command.Login, command.Password, command.Email);
        }
    }
}
