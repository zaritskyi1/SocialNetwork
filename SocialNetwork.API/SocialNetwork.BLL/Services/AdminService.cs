using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.DTOs.Role;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResult<UserWithRoleDto>> GetUsersWithRoles(PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var users = await _unitOfWork.UserRepository.GetUsers(queryOptions);

            var paginationResult = await ConvertToUserWithRoleDto(users);

            return paginationResult;
        }

        public async Task EditUserRoles(string userId, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), userId);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames ??= new string[] { };

            var addingResult = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!addingResult.Succeeded)
            {
                throw new IdentityException(addingResult.Errors);
            }

            var removingResult = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!removingResult.Succeeded)
            {
                throw new IdentityException(removingResult.Errors);
            }
        }

        private async Task<PaginationResult<UserWithRoleDto>> ConvertToUserWithRoleDto(PagedList<User> users)
        {
            var paginationResult = _mapper.Map<PaginationResult<UserWithRoleDto>>(users);

            for (int i = 0; i < paginationResult.Result.Count; i++)
            {
                paginationResult.Result[i].Roles = await _userManager.GetRolesAsync(users.Result[i]);
            }

            return paginationResult;
        }
    }
}
