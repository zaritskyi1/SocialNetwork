using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SocialNetworkContext _context;

        public MessageRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public void UpdateMessage(Message message)
        {
            _context.Update(message);
        }

        public Task<Message> GetMessageById(string id)
        {
            return _context.Messages.Include(m => m.Conversation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<PagedList<Message>> GetMessagesByConversationId(string id, QueryOptions queryOptions)
        {
            var query = _context.Messages
                .Where(m => m.ConversationId == id)
                .OrderByDescending(m => m.CreatedDate)
                .AsQueryable();

            return PagedList<Message>.CreateAsync(query, queryOptions);
        }
    }
}
