using System;

namespace SocialNetwork.DAL.Models
{
    public class MessageReport
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string MessageId { get; set; }

        public User User { get; set; }
        public Message Message { get; set; }

        public MessageReport()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
