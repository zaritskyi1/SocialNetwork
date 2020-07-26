using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IParticipantRepository
    {
        Task<Participant> GetParticipantByUserConversationId(string userId, string conversationId);
        Task<PagedList<Participant>> GetPagedParticipantsByConversationId(string conversationId, QueryOptions queryOptions);
        Task<bool> IsParticipantExistsByUserConversationId(string userId, string conversationId);
    }
}
