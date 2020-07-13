using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IMessageReportRepository
    {
        void AddMessageReport(MessageReport messageReport);
        void DeleteMessageReport(MessageReport messageReport);
        Task<MessageReport> GetMessageReportById(string id);
        Task<PagedList<MessageReport>> GetMessageReports(QueryOptions queryOptions);
    }
}
