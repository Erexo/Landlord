using Infrastructure.DTO;

namespace Infrastructure.Services
{
    public interface IJwtHandler : IService
    {
        JwtDTO CreateToken(string login);
    }
}
