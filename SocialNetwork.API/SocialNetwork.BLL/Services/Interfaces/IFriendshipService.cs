using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.Helpers;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IFriendshipService
    {
        Task<PaginationResult<FriendshipForListDto>> GetUserFriends(string userId, PaginationQuery paginationQuery);
        Task<PaginationResult<FriendshipForListDto>> GetUserFriendshipsRequests(string userId, PaginationQuery paginationQuery);
        Task AddFriendshipRequest(FriendshipDto friendshipDto);
        Task AcceptFriendshipRequest(string userId, string friendshipId);
        Task DeclineFriendshipRequest(string userId, string friendshipId);
    }
}
