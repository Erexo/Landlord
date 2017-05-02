using Infrastructure.DTO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetAsync(string login);
        Task RegisterAsync(string login, string password, string email);
        Task RemoveAsync(string login, string password);
    }
}
