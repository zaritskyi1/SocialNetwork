using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly SocialNetworkContext _context;

        public ParticipantRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public Task<Participant> GetParticipantByUserConversationId(string userId, string conversationId)
        {
            return _context.Participants.FirstOrDefaultAsync(p =>
                p.UserId == userId && p.ConversationId == conversationId);
        }

        public Task<List<Participant>> GetParticipantsByConversationId(string conversationId)
        {
            return _context.Participants
                .Where(p => p.ConversationId == conversationId)
                .Include(p => p.User)
                .ToListAsync();
        }

        public Task<PagedList<Participant>> GetPagedParticipantsByConversationId(string conversationId, QueryOptions queryOptions)
        {
            var query = _context.Participants
                .Where(p => p.ConversationId == conversationId)
                .Include(p => p.User)
                .AsQueryable();

            return PagedList<Participant>.CreateAsync(query, queryOptions);
        }
    }
}
