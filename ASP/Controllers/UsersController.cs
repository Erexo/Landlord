using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Infrastructure.DTO;
using Infrastructure.Commands.Users;
using System.Threading.Tasks;
using Infrastructure.Commands;

namespace ASP.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> Get(string login)
        {
            UserDTO user = await _userService.GetAsync(login);

            if (user == null)
                return NotFound();

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Created($"users/{command.Login}", new object());
        }

        /*[HttpPost]
        public async Task<IActionResult> Post(string login, string password, string email)
        {
            await _userService.RegisterAsync(login, password, email);
            return Created($"users/{login}", new object());
        }*/
    }
}
