using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.Helpers;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<PaginationResult<FriendshipForListDto>> GetUserFriends(string userId, PaginationQuery paginationQuery);
        Task<FriendshipDto> CreateFriendshipRequest(FriendshipForCreationDto friendshipForCreation);
        Task AcceptFriendshipRequest(string userId, string friendshipId);
        Task<FriendshipDto> GetFriendshipById(string userId, string friendshipId);
        Task<FriendshipDto> GetFriendshipByUserIds(string currentUserId, string otherUserId);
        Task DeleteFriendship(string userId, string friendshipId);
        Task<PaginationResult<FriendshipForListDto>> GetUserFriendshipsRequests(string userId, 
            PaginationQuery paginationQuery);
    }
}
