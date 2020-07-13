using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.DTOs.Conversation
{
    public class ConversationForCreationDto
    {
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string FirstUserId { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string SecondUserId { get; set; }
    }
}
