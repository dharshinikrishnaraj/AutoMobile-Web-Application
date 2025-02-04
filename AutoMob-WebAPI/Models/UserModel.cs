using System.ComponentModel.DataAnnotations;

namespace AutoMob_WebAPI.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
