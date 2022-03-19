using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _dbContext;
        public AccountController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto Vm )
        {
            if ( await UserExists(Vm.Username))
            {
                return BadRequest("User Is Already Taken Before");
            }
            using var Hmac = new HMACSHA512();
            AppUser User = new AppUser()
            {
                UserName = Vm.Username,
                PasswordHashed = Hmac.ComputeHash(Encoding.UTF8.GetBytes(Vm.Password)),
                PasswordSalt = Hmac.Key
            };
            _dbContext.Users.Add(User);
          await  _dbContext.SaveChangesAsync();
            return User ;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == username);
        }





        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto Vm)
        {
           var User =  await _dbContext.Users.SingleOrDefaultAsync(x => x.UserName == Vm.Username);
            if (User == null)
            {
                return Unauthorized("User Is Unauthorized");
            }
            using var Hmac = new HMACSHA512(User.PasswordSalt);
            
            var PWComputedHash = Hmac.ComputeHash( Encoding.UTF8.GetBytes(Vm.Password) );
            for (int i = 0; i < PWComputedHash.Length; i++)
            {
                if (PWComputedHash[i] != User.PasswordHashed[i])
                {
                    // password is invalid but i do this msg to confuse hackers
                    return Unauthorized("User Is Unauthorized user or Password is Invalid");
                }
            }
            return User;
        }



    }
}
