using System;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork,
            IConversationService conversationService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _conversationService = conversationService;
            _mapper = mapper;
        }

        public async Task<MessageDto> CreateMessage(MessageForCreation messageForCreation)
        {
            if (!await _unitOfWork.ConversationRepository
                .IsConversationExistsById(messageForCreation.ConversationId))
            {
                
            }

            var message = _mapper.Map<Message>(messageForCreation);
            message.CreatedDate = DateTime.Now;

            _unitOfWork.MessageRepository.AddMessage(message);
            await _conversationService.MarkConversationAsUnreadExceptUser(messageForCreation.UserId,
                messageForCreation.ConversationId);

            var conversation = await _unitOfWork.ConversationRepository.GetConversationById(message.ConversationId);
            conversation.LastMessageDate = DateTime.Now;

            await _unitOfWork.Commit();

            var messageDto = _mapper.Map<MessageDto>(message);
            return messageDto;
        }

        public async Task<MessageDto> GetMessage(string userId, string messageId)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(messageId);

            if (message == null)
            {
                
            }

            if (message.UserId != userId)
            {
                
            }

            var messageDto = _mapper.Map<MessageDto>(message);

            return messageDto;
        }

        public async Task DeleteMessage(string userId, string messageId)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(messageId);

            if (message == null)
            {

            }

            if (message.UserId != userId)
            {

            }

            _unitOfWork.MessageRepository.DeleteMessage(message);
            await _unitOfWork.Commit();
        }
    }
}
