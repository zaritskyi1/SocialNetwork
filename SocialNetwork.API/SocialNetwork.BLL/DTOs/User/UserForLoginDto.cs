using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
