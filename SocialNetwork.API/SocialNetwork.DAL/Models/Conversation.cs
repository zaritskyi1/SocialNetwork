using System;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Models
{
    public class Conversation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime LastMessageDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<Participant> Participants { get; set; }

        public Conversation()
        {
            Id = Guid.NewGuid().ToString();
            LastMessageDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
