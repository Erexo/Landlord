namespace Infrastructure.Services
{
    public interface IEncrypter : IService
    {
        string GetSalt(string password);
        string GetHash(string password, string salt);
    }
}
