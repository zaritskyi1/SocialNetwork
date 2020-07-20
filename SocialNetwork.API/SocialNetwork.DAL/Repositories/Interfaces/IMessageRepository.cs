using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessageById(string id);
        Task<PagedList<Message>> GetMessagesByConversationId(string id, QueryOptions queryOptions);
    }
}
