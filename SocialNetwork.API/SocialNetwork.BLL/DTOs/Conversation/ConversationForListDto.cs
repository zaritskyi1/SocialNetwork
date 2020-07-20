using System;

namespace SocialNetwork.BLL.DTOs.Conversation
{
    public class ConversationForListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime LastMessageDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsUnread { get; set; }
    }
}
