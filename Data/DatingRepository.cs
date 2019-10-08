using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGameDating.api.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameDating.api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _repo;

        public DatingRepository(DataContext repo)
        {
            _repo = repo;
        }
        public void Add<T>(T ntt) where T : class
        {
            _repo.Add(ntt);
        }

        public void Delete<T>(T ntt) where T : class
        {
            _repo.Remove(ntt);
        }

        public async Task<User> GetUser(int id)
        {
            return await _repo.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repo.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _repo.SaveChangesAsync() > 0;   
        }
    }
}