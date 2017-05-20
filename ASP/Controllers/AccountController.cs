using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.Controllers
{

    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;

        public AccountController(IUserService userService, ICommandDispatcher commandDispatcher, IJwtHandler jwtHandler)
            : base(commandDispatcher)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }

        [Route("token")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;
            var token = _jwtHandler.CreateToken("testo");
            return Json(token);
        }
    }
}
