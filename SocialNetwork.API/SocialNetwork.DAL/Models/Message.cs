using System;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }

        public User User { get; set; }
        public Conversation Conversation { get; set; }
        public ICollection<MessageReport> MessageReports { get; set; }

        public Message()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
