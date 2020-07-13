using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.MessageReport;
using SocialNetwork.BLL.Helpers;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IReportService
    {
        Task CreateMessageReport(string userId, string messageId);
        Task RemoveMessageReport(string messageId);
        Task AcceptMessageReport(string messageId);
        Task<PaginationResult<MessageReportForList>> GetReportMessages(PaginationQuery paginationQuery);
    }
}
