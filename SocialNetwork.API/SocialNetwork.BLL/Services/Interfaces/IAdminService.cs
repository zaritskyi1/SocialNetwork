using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.Role;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<PaginationResult<UserWithRoleDto>> GetUsersWithRoles(PaginationQuery paginationQuery);
        Task EditUserRoles(string userId, RoleEditDto roleEditDto);
    }
}
