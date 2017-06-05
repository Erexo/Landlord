using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace ASP.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache memoryCache) 
            : base(commandDispatcher)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginUser command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var token = _memoryCache.Get<JwtDTO>(command.TokenId);

            return Json(token);
        }
    }
}
