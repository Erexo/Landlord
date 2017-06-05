using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.DTO;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.Controllers
{
    public class UsersController : ApiControllerBase
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
            await DispatchAsync(command);

            return Created($"users/{command.Login}", null);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]RemoveUser command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
