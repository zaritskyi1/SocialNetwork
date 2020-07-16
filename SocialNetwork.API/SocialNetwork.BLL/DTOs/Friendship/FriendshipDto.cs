namespace SocialNetwork.BLL.DTOs.Friendship
{
    public class FriendshipDto
    {
        public string Id { get; set; }
        public string SenderId { get; set; } 
        public string ReceiverId { get; set; } 
        public string Status { get; set; }
    }
}
