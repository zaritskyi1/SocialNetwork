using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResult<UserDto>> GetUsers(PaginationQuery paginationQuery)
        {
            var queryOptions = _mapper.Map<QueryOptions>(paginationQuery);

            var users = await _unitOfWork.UserRepository.GetUsers(queryOptions);

            var paginationResult = _mapper.Map<PaginationResult<UserDto>>(users);

            return paginationResult;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task UpdateUserInformation(string id, UserForUpdateDto userForUpdate)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            _mapper.Map(userForUpdate, user);

            await _unitOfWork.Commit();
        }

        public async Task UpdateUserActivity(string id, DateTime date)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            user.LastActive = date;

            await _unitOfWork.Commit();
        }
    }
}
