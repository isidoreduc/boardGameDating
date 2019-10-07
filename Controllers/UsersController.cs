using System.Threading.Tasks;
using BoardGameDating.api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameDating.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DataContext _dbContext;

        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> GetUsers() => 
            Ok(await _dbContext.Users.ToListAsync());

        public async Task<IActionResult> GetUser(int id) => 
            Ok(await _dbContext.Users.FirstOrDefaultAsync(item => item.Id == id));


    }
}