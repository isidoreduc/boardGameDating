using System.Threading.Tasks;
using BoardGameDating.api.Models;

namespace BoardGameDating.api.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}