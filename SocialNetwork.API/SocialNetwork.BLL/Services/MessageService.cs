using System.Collections.Generic;
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
            _unitOfWork.MessageRepository.AddMessage(message);

            var conversation = await _unitOfWork.ConversationRepository.GetConversationById(message.ConversationId);
            
            UpdateConversationWithMessage(message, conversation);

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

        private void UpdateConversationWithMessage(Message message, Conversation conversation)
        {
            conversation.LastMessageDate = message.CreatedDate;

            var participants = conversation.Participants;

            UpdateParticipantsHasUnreadExceptUserId(message.UserId, participants);
        }

        private void UpdateParticipantsHasUnreadExceptUserId(string userId, IEnumerable<Participant> participants)
        {
            foreach (var participant in participants)
            {
                if (participant.UserId != userId)
                {
                    participant.HasUnreadMessages = true;
                }
            }
        }
    }
}
