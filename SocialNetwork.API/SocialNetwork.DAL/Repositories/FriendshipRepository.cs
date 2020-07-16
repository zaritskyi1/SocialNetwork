using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly SocialNetworkContext _context;

        public FriendshipRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public Task<Friendship> GetFriendshipById(string id)
        {
            return _context.Friendships.FirstOrDefaultAsync(f => f.Id == id);
        }

        public Task<bool> IsFriendshipExistsBySenderAndReceiverId(
            string senderId, string receiverId)
        {
            return _context.Friendships.AnyAsync(f =>
                f.SenderId == senderId && f.ReceiverId == receiverId ||
                f.SenderId == receiverId && f.ReceiverId == senderId);
        }

        public void AddFriendship(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
        }

        public void DeleteFriendship(Friendship friendship)
        {
            _context.Friendships.Remove(friendship);
        }

        public Task<PagedList<Friendship>> GetAcceptedFriendshipsByUserId(
            string userId, QueryOptions queryOptions)
        {
            var query = _context.Friendships
                .Where(f => (f.SenderId == userId || f.ReceiverId == userId)
                            && f.Status == FriendshipStatus.Accepted)
                .Include(f => f.Sender)
                .Include(f => f.Receiver)
                .OrderByDescending(f => f.StatusChangedDate)
                .AsQueryable();

            return PagedList<Friendship>.CreateAsync(query, queryOptions);
        }

        public Task<PagedList<Friendship>> GetPendingFriendshipsByReceiverId(
            string receiverId, QueryOptions queryOptions)
        {
            var query = _context.Friendships
                .Where(f => f.ReceiverId == receiverId && f.Status == FriendshipStatus.Pending)
                .Include(f => f.Sender)
                .OrderByDescending(f => f.StatusChangedDate)
                .AsQueryable();

            return PagedList<Friendship>.CreateAsync(query, queryOptions);
        }

        public Task<Friendship> GetFriendshipBySenderAndReceiverId(string senderId, string receiverId)
        {
            return _context.Friendships.FirstOrDefaultAsync(f =>
                f.SenderId == senderId && f.ReceiverId == receiverId ||
                f.SenderId == receiverId && f.ReceiverId == senderId);
        }
    }
}
