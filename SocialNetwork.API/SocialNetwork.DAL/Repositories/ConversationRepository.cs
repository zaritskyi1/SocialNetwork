using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly SocialNetworkContext _context;

        public ConversationRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public void AddConversation(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
        }

        public void DeleteConversation(Conversation conversation)
        {
            _context.Conversations.Remove(conversation);
        }

        public void UpdateConversation(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
        }

        public Task<bool> IsConversationExistsById(string id)
        {
            return _context.Conversations.AnyAsync(c => c.Id == id);
        }

        public Task<bool> IsConversationExistsByUsersId(string firstUserId, string secondUserId)
        {
            return _context.Conversations
                .AnyAsync(c => c.Participants.All(
                    p => p.UserId == firstUserId 
                         || p.UserId == secondUserId));
        }

        public Task<Conversation> GetConversationById(string id)
        {
            return _context.Conversations
                .Include(c => c.Participants)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<PagedList<Conversation>> GetConversationsByUserId(string userId, QueryOptions queryOptions)
        {
            var query = _context.Conversations
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .OrderByDescending(c => c.LastMessageDate)
                .AsQueryable();

            //var query = from conversation in _context.Conversations
            //        join participant in _context.Participants
            //            on new { ConversationId = conversation.Id, UserId = userId }
            //            equals new { participant.ConversationId, participant.UserId }
            //        select conversation; //TODO: remove comment

            query = query
                .Include(c => c.Participants)
                .ThenInclude(p => p.User);

            return PagedList<Conversation>.CreateAsync(query, queryOptions);
        }

        public Task<Conversation> GetConversationByUserIds(string firstUserId, string secondUserId)
        {
            return _context.Conversations
                .FirstOrDefaultAsync(c => c.Participants
                    .All(p => p.UserId == firstUserId 
                              || p.UserId == secondUserId));
        }
    }
}
