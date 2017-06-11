using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Func<Task> runAsync);
    }
}
