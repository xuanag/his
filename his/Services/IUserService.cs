using his.Models;

namespace his.Services
{
    public interface IUserService: IMongoService<User>
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
