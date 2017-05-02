using Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    public abstract class ControllerBase : Controller
    {

        protected readonly ICommandDispatcher CommandDispatcher;

        protected ControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}
