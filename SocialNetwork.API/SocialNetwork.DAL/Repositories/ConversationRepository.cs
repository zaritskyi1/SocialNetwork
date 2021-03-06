﻿using System.Linq;
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

        public Task<bool> IsConversationExistsById(string id)
        {
            return _context.Conversations.AnyAsync(c => c.Id == id);
        }

        public Task<bool> IsConversationExistsByUsersId(string firstUserId, string secondUserId)
        {
            return _context.Conversations
                .AnyAsync(c => c.Participants.All(
                    p => p.UserId == firstUserId || p.UserId == secondUserId));
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
                .Include(c => c.Participants)
                .ThenInclude(p => p.User)
                .AsQueryable();

            return PagedList<Conversation>.CreateAsync(query, queryOptions);
        }

        public Task<Conversation> GetConversationByUserIds(string firstUserId, string secondUserId)
        {
            return _context.Conversations
                .FirstOrDefaultAsync(c => c.Participants
                    .All(p => p.UserId == firstUserId || p.UserId == secondUserId));
        }
    }
}
