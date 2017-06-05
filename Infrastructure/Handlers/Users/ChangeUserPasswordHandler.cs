using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using NLog;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        private readonly IUserService _userService;
        private readonly IEncrypter _encrypter;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ChangeUserPasswordHandler(IUserService userService, IEncrypter encrypter)
        {
            _userService = userService;
            _encrypter = encrypter;
        }

        public async Task HandleAsync(ChangeUserPassword command)
        {
            var user = await _userService.GetExtendedAsync(command.Login);

            if (user == null)
                throw new Exception($"User '{command.Login}' does not exists.");
            if (user.Password != _encrypter.GetHash(command.Password, user.Salt))
                throw new Exception($"Passwords for user '{command.Login} does not match.");
            if (user.Password == _encrypter.GetHash(command.NewPassword, user.Salt))
                throw new Exception($"New password for user '{command.Login} is the same as old password.");

            logger.Info("{0} changed password.", user.Login);
            //TODO: Change Password
        }
    }
}
