namespace SocialNetwork.DAL.Models
{
    public class Participant
    {
        public bool HasUnreadMessages { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }

        public User User { get; set; }
        public Conversation Conversation { get; set; }
    }
}
