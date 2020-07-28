using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.DTOs.Participant;
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

        public async Task<ConversationForListDto> CreateConversation(string userId, ConversationForCreationDto conversationForCreationDto)
        {
            if (conversationForCreationDto.FirstUserId != userId)
            {
                throw new InvalidUserIdException(typeof(Conversation));
            }

            if (conversationForCreationDto.FirstUserId == conversationForCreationDto.SecondUserId)
            {
                throw new ModelValidationException("Can't create Conversation with the same user IDs.");
            }

            var conversationExisting = await _unitOfWork.ConversationRepository.IsConversationExistsByUsersId(
                conversationForCreationDto.FirstUserId, conversationForCreationDto.SecondUserId);

            if (conversationExisting)
            {
                throw new EntityExistsException(typeof(Conversation));
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

            var paginationResult = ConvertToConversationForList(userId, conversations);

            return paginationResult;
        }

        public async Task<PaginationResult<MessageDto>> GetConversationMessages(string userId, string conversationId, PaginationQuery paginationQuery)
        {
            var conversationExisting = await _unitOfWork.ConversationRepository.IsConversationExistsById(conversationId);

            if (!conversationExisting)
            {
                throw new EntityNotFoundException(typeof(Conversation), conversationId);
            }

            await CheckUserConversationAccess(userId, conversationId);

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

            await CheckUserConversationAccess(userId, conversationId);

            var conversationForList = ConvertToConversationForList(userId, conversation);

            return conversationForList;
        }

        public async Task<ConversationForListDto> GetConversationByUsersId(string firstUserId, string secondUserId)
        {
            var conversation = await _unitOfWork.ConversationRepository.GetConversationByUserIds(firstUserId, secondUserId);
            if (conversation == null)
            {
                throw new EntityNotFoundException("Conversation with users IDs doesn't exist.", typeof(Conversation));
            }

            var conversationForList = _mapper.Map<Conversation, ConversationForListDto>(conversation);

            return conversationForList;
        }

        public async Task<PaginationResult<ParticipantDto>> GetConversationParticipants(string userId,
            string conversationId, PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var conversationExists = await _unitOfWork.ConversationRepository.IsConversationExistsById(conversationId);
            if (!conversationExists)
            {
                throw new EntityNotFoundException(typeof(Conversation), conversationId);
            }

            await CheckUserConversationAccess(userId, conversationId);

            var participants = await _unitOfWork.ParticipantRepository
                .GetPagedParticipantsByConversationId(conversationId, queryOptions);

            var paginationResult = _mapper.Map<PaginationResult<ParticipantDto>>(participants);

            return paginationResult;
        }

        public async Task MarkConversationAsRead(string userId, string conversationId)
        {
            var participant =
                await _unitOfWork.ParticipantRepository.GetParticipantByUserConversationId(userId, conversationId);

            if (participant == null)
            {
                throw new AccessDeniedException(typeof(Conversation));
            }

            await CheckUserConversationAccess(userId, conversationId);

            participant.HasUnreadMessages = false;

            await _unitOfWork.Commit();
        }

        private PaginationResult<ConversationForListDto> ConvertToConversationForList(string userId, PagedList<Conversation> pagedConversations)
        {
            var paginationResult = _mapper.Map<PaginationResult<ConversationForListDto>>(pagedConversations);
            var conversations = pagedConversations.Result;

            for (var i = 0; i < conversations.Count; i++)
            {
                var participantsDto = conversations[i].Participants;

                foreach (var participantDto in participantsDto)
                {
                    if (participantDto.UserId == userId)
                    {
                        paginationResult.Result[i].IsUnread = participantDto.HasUnreadMessages;
                    }
                    else
                    {
                        paginationResult.Result[i].Title = participantDto.User.Name + " " + participantDto.User.Surname;
                    }
                }
            }

            return paginationResult;
        }

        private ConversationForListDto ConvertToConversationForList(string userId, Conversation conversation)
        {
            var conversationForList = _mapper.Map<Conversation, ConversationForListDto>(conversation);

            foreach (var participant in conversation.Participants)
            {
                if (participant.UserId != userId)
                {
                    conversationForList.Title = participant.User.Name + " " + participant.User.Surname;
                }
                else
                {
                    conversationForList.IsUnread = participant.HasUnreadMessages;
                }
            }

            return conversationForList;
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
    }
}
