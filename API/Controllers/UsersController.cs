using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public UsersController( DataContext dbContext )
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task< ActionResult<List<AppUser>>> GetAllUsers()
        {
            var Users = await _dbContext.Users.ToListAsync();
            return Users;
        }

        [HttpGet]
        [Route("GetUsers/{Id}")]
        public async Task<ActionResult<AppUser>> GetUser(int Id)
        {
            var User = await _dbContext.Users.FirstOrDefaultAsync(_=>_.Id == Id);
            return User;
        }
    }
}
