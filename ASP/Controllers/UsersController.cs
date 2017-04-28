using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Infrastructure.DTO;
using Infrastructure.Commands.Users;
using System.Threading.Tasks;
using Infrastructure.Commands;

namespace ASP.Controllers
{
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
            : base (commandDispatcher)
        {
            _userService = userService;
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
            await CommandDispatcher.DispatchAsync(command);

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
