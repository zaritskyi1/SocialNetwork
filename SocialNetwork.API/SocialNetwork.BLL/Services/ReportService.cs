using System;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BLL.DTOs.MessageReport;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateMessageReport(string userId, string messageId)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(messageId);

            if (message == null)
            {
                throw new EntityNotFoundException(typeof(Message), messageId);
            }

            var userExisting = await _unitOfWork.UserRepository.IsUserWithIdExists(userId);
            if (!userExisting)
            {
                throw new EntityNotFoundException(typeof(User), userId);
            }

            var messageReport = new MessageReport()
            {
                CreatedDate = DateTime.Now,
                MessageId = messageId,
                UserId = userId
            };

            _unitOfWork.MessageReportRepository.AddMessageReport(messageReport);
            await _unitOfWork.Commit();
        }

        public async Task RemoveMessageReport(string messageId)
        {
            var message = await _unitOfWork.MessageReportRepository.GetMessageReportById(messageId);

            if (message == null)
            {
                throw new EntityNotFoundException(typeof(MessageReport), messageId);
            }

            _unitOfWork.MessageReportRepository.DeleteMessageReport(message);
            await _unitOfWork.Commit();
        }

        public async Task AcceptMessageReport(string messageId)
        {
            var messageReport = await _unitOfWork.MessageReportRepository.GetMessageReportById(messageId);

            if (messageReport == null)
            {
                throw new EntityNotFoundException(typeof(MessageReport), messageId);
            }

            var message = await _unitOfWork.MessageRepository.GetMessageById(messageReport.MessageId);

            _unitOfWork.MessageRepository.DeleteMessage(message);
            await _unitOfWork.Commit();
        }

        public async Task<PaginationResult<MessageReportForList>> GetReportMessages(PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var reportMessages = await _unitOfWork.MessageReportRepository.GetMessageReports(queryOptions);

            var paginationResult = _mapper.Map<PaginationResult<MessageReportForList>>(reportMessages);

            return paginationResult;
        }
    }
}
