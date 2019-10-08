using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGameDating.api.Models;

namespace BoardGameDating.api.Data
{
    public interface IDatingRepository
    {
        // generic interface to be implemented for entities we want to expose
         void Add<T>(T ntt) where T : class;
         void Delete<T>(T ntt) where T : class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}