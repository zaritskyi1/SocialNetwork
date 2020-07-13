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

        public void AddParticipant(Participant participant)
        {
            _context.Participants.Add(participant);
        }

        public void DeleteParticipant(Participant participant)
        {
            _context.Participants.Add(participant);
        }

        public void UpdateParticipant(Participant participant)
        {
            _context.Participants.Update(participant);
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
                .ToListAsync();
        }

        public Task<PagedList<Participant>> GetPagedParticipantsByConversationId(string conversationId, QueryOptions queryOptions)
        {
            var query = _context.Participants
                .Where(p => p.ConversationId == conversationId)
                .AsQueryable();

            return PagedList<Participant>.CreateAsync(query, queryOptions);
        }

        public Task<PagedList<Participant>> GetPagedParticipantsByUserId(string userId, QueryOptions queryOptions)
        {
            var query = _context.Participants
                .Where(p => p.UserId == userId)
                .AsQueryable();

            return PagedList<Participant>.CreateAsync(query, queryOptions);
        }
    }
}
