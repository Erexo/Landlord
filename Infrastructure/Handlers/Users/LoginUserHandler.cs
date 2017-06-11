using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class LoginUserHandler : ICommandHandler<LoginUser>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _memoryCache;
        private readonly IHandler _handler;

        public LoginUserHandler(IUserService userService, IJwtHandler jwtHandler, 
            IMemoryCache memoryCache, IHandler handler)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _memoryCache = memoryCache;
            _handler = handler;
        }

        public async Task HandleAsync(LoginUser command)
        {
            _handler.Run(async () => { await _userService.LoginAsync(command.Login, command.Password); })
                .Next()
                .Run(async () =>
                {
                    var token = _jwtHandler.CreateToken(command.Login);
                    _memoryCache.Set(command.TokenId, token, TimeSpan.FromSeconds(5));
                    await Task.CompletedTask;
                });
            await _handler.ExecuteAllAsync();
            
        }
    }
}
