using System;

namespace SocialNetwork.BLL.DTOs.Message
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }
        public string ConversationId { get; set; }
    }
}
