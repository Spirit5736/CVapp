using Microsoft.Build.Framework;

namespace API.models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}