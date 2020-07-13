using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ConversationForListDto> CreateConversation(ConversationForCreationDto conversationForCreationDto)
        {
            if (conversationForCreationDto.FirstUserId == conversationForCreationDto.SecondUserId)
            {
                //TODO: EXCEPTION
            }

            var conversationExisting = await _unitOfWork.ConversationRepository.IsConversationExistsByUsersId(
                conversationForCreationDto.FirstUserId, conversationForCreationDto.SecondUserId);

            if (conversationExisting)
            {
                throw new Exception("Already exists");
            }

            var conversation = _mapper.Map<ConversationForCreationDto, Conversation>(conversationForCreationDto);

            _unitOfWork.ConversationRepository.AddConversation(conversation);
            await _unitOfWork.Commit();

            var conversationForListDto = _mapper.Map<Conversation, ConversationForListDto>(conversation);
            return conversationForListDto;
        }

        public async Task<PaginationResult<ConversationForListDto>> GetPaginatedConversationsByUserId(string userId, PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var conversations = await _unitOfWork.ConversationRepository.GetConversationsByUserId(userId, queryOptions);

            var paginationResult = _mapper.Map<PaginationResult<ConversationForListDto>>(conversations);

            return paginationResult;
        }

        public async Task<PaginationResult<MessageDto>> GetConversationMessages(string userId, string conversationId, PaginationQuery paginationQuery)
        {
            var conversation = await _unitOfWork.ConversationRepository.GetConversationById(conversationId);

            if (conversation == null)
            {
                throw new EntityNotFoundException(typeof(Conversation), conversationId);
            }

            if (conversation.Participants.All(p => p.UserId != userId))
            {
                throw new AccessDeniedException(typeof(Conversation));
            }

            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);
            var messages = await _unitOfWork.MessageRepository.GetMessagesByConversationId(conversationId, queryOptions);

            var paginationResult = _mapper.Map<PaginationResult<MessageDto>>(messages);

            return paginationResult;
        }

        public async Task<ConversationForListDto> GetConversationById(string userId, string conversationId)
        {
            var conversation = await _unitOfWork.ConversationRepository.GetConversationById(conversationId);

            if (conversation == null)
            {
                throw new EntityNotFoundException(typeof(Conversation), conversationId);
            }

            if (conversation.Participants.All(p => p.UserId != userId))
            {
                throw new AccessDeniedException(typeof(Conversation));
            }

            var conversationForList = _mapper.Map<Conversation, ConversationForListDto>(conversation);

            return conversationForList;
        }

        public async Task<ConversationForListDto> GetConversationByUsersId(string firstUserId, string secondUserId)
        {
            var conversation = await _unitOfWork.ConversationRepository.GetConversationByUserIds(firstUserId, secondUserId);

            var conversationForList = _mapper.Map<Conversation, ConversationForListDto>(conversation);

            return conversationForList;
        }

        public async Task MarkConversationAsUnreadExceptUser(string userId, string conversationId)
        {
            var participants = await _unitOfWork.ParticipantRepository.GetParticipantsByConversationId(conversationId);

            foreach (var participant in participants)
            {
                if (participant.UserId != userId)
                {
                    participant.HasUnreadMessages = true;
                }
            }

            await _unitOfWork.Commit();
        }

        public async Task MarkConversationAsRead(string userId, string conversationId)
        {
            var participants = await _unitOfWork.ParticipantRepository.GetParticipantsByConversationId(conversationId);

            foreach (var participant in participants)
            {
                if (participant.UserId == userId)
                {
                    participant.HasUnreadMessages = false;
                    await _unitOfWork.Commit();
                    return;
                }
            }
        }
    }
}
