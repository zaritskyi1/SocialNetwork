using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.DTOs.Message
{
    public class MessageForCreationDto
    {
        [Required]
        [StringLength(512, MinimumLength = 1)]
        public string Content { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string ConversationId { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string UserId { get; set; }
    }
}
