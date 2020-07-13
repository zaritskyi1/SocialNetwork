using System;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.DTOs.User;

namespace SocialNetwork.BLL.DTOs.MessageReport
{
    public class MessageReportForList
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string MessageId { get; set; }

        public UserForListDto User { get; set; }
        public MessageDto Message { get; set; }
    }
}
