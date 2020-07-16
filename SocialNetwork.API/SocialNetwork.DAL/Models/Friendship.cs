using System;

namespace SocialNetwork.DAL.Models
{
    public enum FriendshipStatus
    {
        Pending,
        Accepted
    }

    public class Friendship
    {
        public string Id { get; set; }
        public string SenderId { get; set; } 
        public string ReceiverId { get; set; } 
        public FriendshipStatus Status { get; set; }
        public DateTime StatusChangedDate { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }

        public Friendship()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
