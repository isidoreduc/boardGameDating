using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BoardGameDating.api.Data;
using BoardGameDating.api.Models.DTOs;
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
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() => 
            Ok(_mapper.Map<IEnumerable<UsersForGetAllDto>>(await _repo.GetUsers()));
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) => 
            Ok( _mapper.Map<UserForGetOneDto>(await _repo.GetUser(id)));


    }
}