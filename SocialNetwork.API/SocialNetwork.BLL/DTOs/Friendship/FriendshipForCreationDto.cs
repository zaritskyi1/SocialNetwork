using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.DTOs.Friendship
{
    public class FriendshipForCreationDto
    {
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string SenderId { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string ReceiverId { get; set; } 
    }
}
