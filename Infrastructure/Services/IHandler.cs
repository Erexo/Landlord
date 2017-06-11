using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IHandler : IService
    {
        IHandlerTask Run(Func<Task> runAsync);
        IHandlerTaskRunner Validate(Func<Task> validateAsync);
        Task ExecuteAllAsync();
    }
}
