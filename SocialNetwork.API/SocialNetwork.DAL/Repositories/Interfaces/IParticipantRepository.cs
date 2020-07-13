using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IParticipantRepository
    {
        void AddParticipant(Participant participant);
        void DeleteParticipant(Participant participant);
        void UpdateParticipant(Participant participant);
        Task<Participant> GetParticipantByUserConversationId(string userId, string conversationId);
        Task<List<Participant>> GetParticipantsByConversationId(string conversationId);
        Task<PagedList<Participant>> GetPagedParticipantsByConversationId(string conversationId, QueryOptions queryOptions);
        Task<PagedList<Participant>> GetPagedParticipantsByUserId(string userId, QueryOptions queryOptions);
    }
}
