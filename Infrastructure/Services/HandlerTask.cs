using System;
using System.Threading.Tasks;
using NLog;

namespace Infrastructure.Services
{
    public class HandlerTask : IHandlerTask
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IHandler _handler;
        private readonly Func<Task> _runAsync;
        private Func<Task> _validateAsync;
        private Func<Task> _alwaysAsync;
        private Func<Task> _onSuccessAsync;
        private Func<Exception, Logger, Task> _onErrorAsync;
        private bool _propagateException = true;

        public HandlerTask(IHandler handler, Func<Task> runAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
        }

        public HandlerTask(IHandler handler, Func<Task> runAsync,
            Func<Task> validateAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
            _validateAsync = validateAsync;
        }

        public IHandlerTask Always(Func<Task> always)
        {
            _alwaysAsync = always;
            return this;
        }

        public IHandlerTask PropagateException()
        {
            _propagateException = true;
            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;
            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccessAsync = onSuccess;
            return this;
        }

        public IHandlerTask OnError(Func<Exception, Logger, Task> onError)
        {
            _onErrorAsync = onError;
            return this;
        }

        public IHandler Next()
            => _handler;

        public async Task ExecuteAsync()
        {
            try
            {
                if (_validateAsync != null)
                    await _validateAsync();

                await _runAsync();

                if (_onSuccessAsync != null)
                    await _onSuccessAsync();
            }
            catch(Exception ex)
            {
                if (_onErrorAsync != null)
                    await _onErrorAsync(ex, logger);

                if (_propagateException)
                    throw;
            }
            finally
            {
                if (_alwaysAsync != null)
                    await _alwaysAsync();
            }
        }
    }
}
