using System;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResult<UserDto>> GetUsers(PaginationQuery paginationQuery);
        Task<UserDto> GetUserById(string id);
        Task UpdateUserInformation(string id, UserForUpdateDto userForUpdate);
        Task UpdateUserActivity(string id, DateTime date);
    }
}
