using System.Threading.Tasks;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetUsers(QueryOptions queryOptions);
        Task<User> GetUserById(string id);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        Task<bool> IsUserWithIdExists(string id);
    }
}
