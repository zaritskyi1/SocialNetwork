using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        void AddConversation(Conversation conversation);
        Task<bool> IsConversationExistsById(string id);
        Task<bool> IsConversationExistsByUsersId(string firstUserId, string secondUserId);
        Task<Conversation> GetConversationById(string id);
        Task<PagedList<Conversation>> GetConversationsByUserId(string userId, QueryOptions queryOptions);
        Task<Conversation> GetConversationByUserIds(string firstUserId, string secondUserId);
    }
}
