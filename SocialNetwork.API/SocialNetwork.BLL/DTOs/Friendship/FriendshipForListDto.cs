using SocialNetwork.BLL.DTOs.User;

namespace SocialNetwork.BLL.DTOs.Friendship
{
    public class FriendshipForListDto
    {
        public string Id { get; set; }
        public UserForListDto Friend { get; set; }
    }
}
