using API.models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserContext userContext;

        public UserController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [HttpPost ("register")]
        public async Task <ActionResult<User>> Register(string username, string password)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
            };

            userContext.Users.Add(user);
            await userContext.SaveChangesAsync();

            return user;
        }

    }
}
