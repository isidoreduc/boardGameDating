using System.Threading.Tasks;
using BoardGameDating.api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGameDating.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IDatingRepository _repo;

        public UsersController(IDatingRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() => 
            Ok(await _repo.GetUsers());
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) => 
            Ok(await _repo.GetUser(id));


    }
}