using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.DTOs.User
{
    public class UserForLoginDto
    {
        [Required]
        [StringLength(16, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
