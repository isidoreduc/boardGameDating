using System.Threading.Tasks;
using BoardGameDating.api.Data;
using BoardGameDating.api.Models;
using BoardGameDating.api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameDating.api.Controllers {
    [Route ("api/[controller]")]
    public class AuthController : Controller {
        private readonly IAuthRepository _authRepo;

        public AuthController (IAuthRepository authRepo) {
            _authRepo = authRepo;
        }

        // FromBody - use the info from the request body (submitted by user)
        [HttpPost ("register")]
        public async Task<IActionResult> Register ([FromBody] UserForRegisterDTO userForRegisterDTO) {
            userForRegisterDTO.Name = userForRegisterDTO.Name.ToLower ();
            // passing the 'existing user' error in the ModelState towards a BadRequest
            if (await _authRepo.UserExists (userForRegisterDTO.Name))
                ModelState.AddModelError("Username", "Username is already taken.");
            
            // data validation = passing the validation error messages in the BadRequest
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var userToCreate = new User () {
                Name = userForRegisterDTO.Name
            };
            var createUser = await _authRepo.Register (userToCreate, userForRegisterDTO.Password);

            return StatusCode (201);
        }
    }
}