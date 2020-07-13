using System;
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
            var user = await _unitOfWork.UserRepository.GetUserById(userId);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), userId);
            }

            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var users = await _unitOfWork.FriendshipRepository.GetAcceptedFriendshipsByUserId(userId, queryOptions);

            var paginationResult = await ConvertToFriendshipForListDto(userId, users);

            return paginationResult;
        }

        public async Task<PaginationResult<FriendshipForListDto>> GetUserFriendshipsRequests(string userId, PaginationQuery paginationQuery)
        {
            if (!await _unitOfWork.UserRepository.IsUserWithIdExists(userId))
            {
                throw new EntityNotFoundException(typeof(User), userId);
            }

            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var users = await _unitOfWork.FriendshipRepository.GetPendingFriendshipsByReceiverId(userId, queryOptions);
            
            var paginationResult = _mapper.Map<PaginationResult<FriendshipForListDto>>(users);

            return paginationResult;
        }

        public async Task AddFriendshipRequest(FriendshipDto friendshipDto)
        {
            if (!await _unitOfWork.UserRepository.IsUserWithIdExists(friendshipDto.ReceiverId))
            {
                throw new EntityNotFoundException(typeof(User), friendshipDto.ReceiverId);
            }

            if (!await _unitOfWork.UserRepository.IsUserWithIdExists(friendshipDto.SenderId))
            {
                throw new EntityNotFoundException(typeof(User), friendshipDto.SenderId);
            }

            
            var friendshipExisting = await _unitOfWork.FriendshipRepository.IsFriendshipExistsBySenderAndReceiverId(
                friendshipDto.SenderId, friendshipDto.ReceiverId);

            if (friendshipExisting)
            {
                // TODO Exception for exist
                throw new Exception();
            }

            var newFriendship = _mapper.Map<Friendship>(friendshipDto);

            newFriendship.Status = FriendshipStatus.Pending;
            newFriendship.StatusChangedDate = DateTime.Now;

            _unitOfWork.FriendshipRepository.AddFriendship(newFriendship);
            await _unitOfWork.Commit();
        }

        public async Task AcceptFriendshipRequest(string userId, string friendshipId)
        {
            if (!await _unitOfWork.UserRepository.IsUserWithIdExists(userId))
            {
                throw new EntityNotFoundException(typeof(User), userId);
            }

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
                throw new AcceptFriendshipOperationException(friendshipId);
            }

            friendship.Status = FriendshipStatus.Accepted;
            friendship.StatusChangedDate = DateTime.Now;

            await _unitOfWork.Commit();
        }

        public async Task DeclineFriendshipRequest(string userId, string friendshipId)
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
                throw new DeclineFriendshipOperationException(friendshipId);
            }

            friendship.Status = FriendshipStatus.Declined;
            friendship.StatusChangedDate = DateTime.Now;

            await _unitOfWork.Commit();
        }

        private async Task<PaginationResult<FriendshipForListDto>> ConvertToFriendshipForListDto(string userId, PagedList<Friendship> friendships)
        {
            var friendshipsForList = _mapper.Map<PaginationResult<FriendshipForListDto>>(friendships);

            for (int i = 0; i < friendships.Result.Count; i++)
            {
                var friendId = friendships.Result[i].SenderId == userId ? friendships.Result[i].SenderId : friendships.Result[i].ReceiverId;

                var friend = await _unitOfWork.UserRepository.GetUserById(friendId);
                
                var friendForListDto = _mapper.Map<UserForListDto>(friend);

                friendshipsForList.Result[i].Friend = friendForListDto;
            }

            return friendshipsForList;
        }
    }
}
