using Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {

        private readonly ICommandDispatcher _commandDispatcher;
        protected string Login
            => User?.Identity?.IsAuthenticated == true ? User.Identity.Name : string.Empty;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.Login = this.Login;
            }

            await _commandDispatcher.DispatchAsync(command);
        }

    }
}
