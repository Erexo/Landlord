using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
        }

        [Authorize]
        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await DispatchAsync(command);
        
            return NoContent();
        }
    }
}
