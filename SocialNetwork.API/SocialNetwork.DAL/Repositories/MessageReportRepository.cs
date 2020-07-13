using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class MessageReportRepository : IMessageReportRepository
    {
        private readonly SocialNetworkContext _context;

        public MessageReportRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public void AddMessageReport(MessageReport messageReport)
        {
            _context.MessageReports.Add(messageReport);
        }

        public void DeleteMessageReport(MessageReport messageReport)
        {
            _context.MessageReports.Remove(messageReport);
        }

        public Task<MessageReport> GetMessageReportById(string id)
        {
            return _context.MessageReports.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<PagedList<MessageReport>> GetMessageReports(QueryOptions queryOptions)
        {
            var query = _context.MessageReports
                .OrderByDescending(m => m.CreatedDate)
                .Include(m => m.User)
                .Include(m => m.Message)
                .AsQueryable();

            return PagedList<MessageReport>.CreateAsync(query, queryOptions);
        }
    }
}
