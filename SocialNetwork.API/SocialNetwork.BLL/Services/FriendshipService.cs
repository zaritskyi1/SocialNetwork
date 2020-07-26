using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FriendshipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResult<FriendshipForListDto>> GetUserFriends(string userId, PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var users = await _unitOfWork.FriendshipRepository.GetAcceptedFriendshipsByUserId(userId, queryOptions);

            var paginationResult = ConvertToFriendshipForListDto(userId, users);

            return paginationResult;
        }

        public async Task<PaginationResult<FriendshipForListDto>> GetUserFriendshipsRequests(string userId, PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var users = await _unitOfWork.FriendshipRepository.GetPendingFriendshipsByReceiverId(userId, queryOptions);
            
            var paginationResult = ConvertToFriendshipForListDto(userId, users);

            return paginationResult;
        }

        public async Task<FriendshipDto> CreateFriendshipRequest(string userId, FriendshipForCreationDto friendshipForCreation)
        {
            await ValidateFriendshipForCreation(userId, friendshipForCreation);

            var friendship = _mapper.Map<Friendship>(friendshipForCreation);
            friendship.Status = FriendshipStatus.Pending;
            friendship.StatusChangedDate = DateTime.Now;

            _unitOfWork.FriendshipRepository.AddFriendship(friendship);
            await _unitOfWork.Commit();

            var friendshipDto = _mapper.Map<FriendshipDto>(friendship);
            return friendshipDto;
        }

        public async Task AcceptFriendshipRequest(string userId, string friendshipId)
        {
            var friendship = await _unitOfWork.FriendshipRepository.GetFriendshipById(friendshipId);

            if (friendship == null)
            {
                throw new EntityNotFoundException(typeof(Friendship), friendshipId);
            }

            if (friendship.ReceiverId != userId)
            {
                throw new AccessDeniedException(typeof(Friendship));
            }

            if (friendship.Status != FriendshipStatus.Pending)
            {
                throw new AcceptFriendshipOperationException(friendship.Id);
            }

            friendship.Status = FriendshipStatus.Accepted;
            friendship.StatusChangedDate = DateTime.Now;

            await _unitOfWork.Commit();
        }

        public async Task<FriendshipDto> GetFriendshipById(string userId, string friendshipId)
        {
            var friendship =
                await _unitOfWork.FriendshipRepository.GetFriendshipById(friendshipId);

            if (friendship == null)
            {
                throw new EntityNotFoundException(typeof(Friendship), friendshipId);
            }

            CheckUserFriendshipAccess(userId, friendship);

            var friendshipDto = _mapper.Map<FriendshipDto>(friendship);

            return friendshipDto;
        }

        public async Task<FriendshipDto> GetFriendshipByUserIds(string currentUserId, string otherUserId)
        {
            var isUserExists = await _unitOfWork.UserRepository.IsUserWithIdExists(otherUserId);
            if (!isUserExists)
            {
                throw new EntityNotFoundException(typeof(User), otherUserId);
            }

            var friendship =
                await _unitOfWork.FriendshipRepository.GetFriendshipBySenderAndReceiverId(currentUserId, otherUserId);

            if (friendship == null)
            {
                throw new EntityNotFoundException("Friendship with users IDs doesn't exist.", typeof(Conversation));
            }

            CheckUserFriendshipAccess(currentUserId, friendship);

            var friendshipDto = _mapper.Map<FriendshipDto>(friendship);
            return friendshipDto;
        }

        public async Task DeleteFriendship(string userId, string friendshipId)
        {
            var friendship = await _unitOfWork.FriendshipRepository.GetFriendshipById(friendshipId);

            if (friendship == null)
            {
                throw new EntityNotFoundException(typeof(Friendship), friendshipId);
            }

            CheckUserFriendshipAccess(userId, friendship);

            _unitOfWork.FriendshipRepository.DeleteFriendship(friendship);

            await _unitOfWork.Commit();
        }

        private PaginationResult<FriendshipForListDto> ConvertToFriendshipForListDto(string userId, PagedList<Friendship> friendships)
        {
            var friendshipsForList = _mapper.Map<PaginationResult<FriendshipForListDto>>(friendships);

            for (int i = 0; i < friendships.Result.Count; i++)
            {
                var friendIsSender = friendships.Result[i].SenderId != userId;

                var friend = friendIsSender ? friendships.Result[i].Sender : friendships.Result[i].Receiver;
                
                var friendForListDto = _mapper.Map<UserForListDto>(friend);

                friendshipsForList.Result[i].Friend = friendForListDto;
            }

            return friendshipsForList;
        }

        private async Task ValidateFriendshipForCreation(string userId, FriendshipForCreationDto friendshipDto)
        {
            if (userId != friendshipDto.SenderId)
            {
                throw new InvalidUserIdException(typeof(Friendship));
            }

            var isUserExists = await _unitOfWork.UserRepository.IsUserWithIdExists(friendshipDto.ReceiverId);

            if (!isUserExists)
            {
                throw new EntityNotFoundException(typeof(User), friendshipDto.ReceiverId);
            }

            if (friendshipDto.ReceiverId == friendshipDto.SenderId)
            {
                throw new ModelValidationException("Can't create Friendship with the same user IDs.");
            }
            
            var friendshipExisting = await _unitOfWork.FriendshipRepository.IsFriendshipExistsBySenderAndReceiverId(
                friendshipDto.SenderId, friendshipDto.ReceiverId);

            if (friendshipExisting)
            {
                throw new EntityExistsException(typeof(Friendship));
            }
        }

        private static void CheckUserFriendshipAccess(string userId, Friendship friendship)
        {
            if (friendship.ReceiverId != userId && friendship.SenderId != userId)
            {
                throw new AccessDeniedException(typeof(Friendship));
            }
        }
    }
}
