using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Infrastructure.DTO;
using Infrastructure.Commands.Users;
using System.Threading.Tasks;
using Infrastructure.Commands;

namespace ASP.Controllers
{

    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
