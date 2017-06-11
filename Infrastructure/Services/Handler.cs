using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class Handler : IHandler
    {
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();

        public async Task ExecuteAllAsync()
        {
            foreach(var handlerTask in _handlerTasks)
                await handlerTask.ExecuteAsync();

            _handlerTasks.Clear();
        }

        public IHandlerTask Run(Func<Task> runAsync)
        {
            HandlerTask handlerTask = new HandlerTask(this, runAsync);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTaskRunner Validate(Func<Task> validateAsync)
            => new HandlerTaskRunner(this, validateAsync, _handlerTasks);
    }
}
