using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.Message;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> CreateMessage(string userId, MessageForCreationDto messageForCreationDto);
        Task<MessageDto> GetMessage(string userId, string messageId);
        Task DeleteMessage(string userId, string messageId);
    }
}
