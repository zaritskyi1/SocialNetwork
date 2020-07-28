using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageDto> CreateMessage(string userId, MessageForCreationDto messageForCreationDto)
        {
            await ValidateMessageForCreation(userId, messageForCreationDto);

            var message = _mapper.Map<Message>(messageForCreationDto);
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

            ValidateMessage(userId, message);

            var messageDto = _mapper.Map<MessageDto>(message);

            return messageDto;
        }

        public async Task DeleteMessage(string userId, string messageId)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(messageId);

            ValidateMessage(userId, message);

            _unitOfWork.MessageRepository.DeleteMessage(message);
            await _unitOfWork.Commit();
        }

        private async Task ValidateMessageForCreation(string userId, MessageForCreationDto messageForCreationDto)
        {
            if (userId != messageForCreationDto.UserId)
            {
                throw new InvalidUserIdException(typeof(Message));
            }

            var conversationExisting =
                await _unitOfWork.ConversationRepository.IsConversationExistsById(messageForCreationDto.ConversationId);
            if (!conversationExisting)
            {
                throw new EntityNotFoundException(typeof(Conversation), messageForCreationDto.ConversationId);
            }

            await CheckUserConversationAccess(userId, messageForCreationDto.ConversationId);
        }

        private async Task CheckUserConversationAccess(string userId, string conversationId)
        {
            var participantExists =
                await _unitOfWork.ParticipantRepository.IsParticipantExistsByUserConversationId(userId, conversationId);

            if (!participantExists)
            {
                throw new AccessDeniedException(typeof(Conversation));
            }
        }

        private static void ValidateMessage(string userId, Message message)
        {
            if (message == null)
            {
                throw new EntityNotFoundException(typeof(Message), userId);
            }

            if (message.UserId != userId)
            {
                throw new AccessDeniedException(typeof(Message));
            }
        }

        private static void UpdateConversationWithMessage(Message message, Conversation conversation)
        {
            conversation.LastMessageDate = message.CreatedDate;

            var participants = conversation.Participants;

            UpdateParticipantsHasUnreadExceptUserId(message.UserId, participants);
        }

        private static void UpdateParticipantsHasUnreadExceptUserId(string userId,
            IEnumerable<Participant> participants)
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
