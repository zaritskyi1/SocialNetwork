using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<Friendship> GetFriendshipById(string id);
        Task<bool> IsFriendshipExistsBySenderAndReceiverId(string senderId, string receiverId);
        void AddFriendship(Friendship friendship);
        void DeleteFriendship(Friendship friendship);
        void UpdateFriendship(Friendship friendship);
        Task<PagedList<Friendship>> GetAcceptedFriendshipsByUserId(string userId, QueryOptions queryOptions);
        Task<PagedList<Friendship>> GetPendingFriendshipsByReceiverId(string receiverId, QueryOptions queryOptions);
    }
}
