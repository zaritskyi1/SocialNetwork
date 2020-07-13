using System;
using System.Collections.Generic;
using SocialNetwork.BLL.DTOs.Participant;

namespace SocialNetwork.BLL.DTOs.Conversation
{
    public class ConversationForListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime LastMessageDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<ParticipantDto> Participants { get; set; }
    }
}
