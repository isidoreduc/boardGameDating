using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoardGameDating.api.Data;
using BoardGameDating.api.Models;
using BoardGameDating.api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BoardGameDating.api.Controllers {
    [Route ("api/[controller]")]
    public class AuthController : Controller {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;

        public AuthController (IAuthRepository authRepo, IConfiguration config) {
            _authRepo = authRepo;
            _config = config;
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

[HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO userForLoginDto)
        {
            // throw new Exception("No to the no");

            var userFromRepo = await _authRepo.Login(userForLoginDto.Name.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value); // accessing the secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Name)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }
    }
}