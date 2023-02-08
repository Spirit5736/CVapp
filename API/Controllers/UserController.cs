using API.DTOs;
using API.models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserContext userContext;
        private readonly ITokenService tokenService;

        public UserController(UserContext userContext, ITokenService tokenService)
        {
            this.userContext = userContext;
            this.tokenService = tokenService;
        }

        // https://localhost:7063/api/user/register
        [HttpPost ("register")]
        public async Task <ActionResult<User>> Register([FromBody]RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            userContext.Users.Add(user);
            await userContext.SaveChangesAsync();

            return user;
        }

        [HttpPost ("login")]
        public async Task <ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userContext.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
            if (user == null) return Unauthorized("invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i] ) return Unauthorized("invalid password");
            } 

             return new UserDto
            {
                Username = user.Username,
                Token = tokenService.CreateToken(user)
            };
        }

        public async Task<bool> UserExists(string username)
        {
            return await userContext.Users.AnyAsync(x => x.Username == username.ToLower());
        }

    }
}
