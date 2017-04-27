using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Infrastructure.DTO;
using Infrastructure.Commands.Users;
using System.Threading.Tasks;
using Infrastructure.Commands;

namespace ASP.Controllers
{

    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountController(IUserService userService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
