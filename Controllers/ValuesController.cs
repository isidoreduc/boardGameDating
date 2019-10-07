using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public ValuesController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues() => 
            Ok(await _dbContext.Values.ToListAsync());
            

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id) =>
            Ok(await _dbContext.Values.FirstOrDefaultAsync(item => item.Id == id));
        

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
