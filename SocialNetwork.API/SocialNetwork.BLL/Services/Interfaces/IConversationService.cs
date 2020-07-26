using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.DTOs.Participant;
using SocialNetwork.BLL.Helpers;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationForListDto> CreateConversation(string userId,
            ConversationForCreationDto conversationForCreationDto);
        Task<PaginationResult<ConversationForListDto>> GetPaginatedConversationsByUserId(
            string userId, PaginationQuery paginationQuery);
        Task<PaginationResult<MessageDto>> GetConversationMessages(
            string userId, string conversationId, PaginationQuery paginationQuery);
        Task<ConversationForListDto> GetConversationById(string userId, string conversationId);
        Task<ConversationForListDto> GetConversationByUsersId(string firstUserId, string secondUserId);
        Task<PaginationResult<ParticipantDto>> GetConversationParticipants(string userId, string conversationId,
            PaginationQuery paginationQuery);
        Task MarkConversationAsRead(string userId, string conversationId);
    }
}
