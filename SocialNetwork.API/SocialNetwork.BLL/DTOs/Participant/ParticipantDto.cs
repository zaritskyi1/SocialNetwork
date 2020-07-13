using SocialNetwork.BLL.DTOs.User;

namespace SocialNetwork.BLL.DTOs.Participant
{
    public class ParticipantDto
    {
        public bool HasUnreadMessages { get; set; }

        public string UserId { get; set; }
        public UserForListDto User { get; set; }
    }
}
