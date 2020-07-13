﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Filters;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserActivityActionFilter))]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFriendshipService _friendshipService;

        public UserController(IUserService userService, 
            IFriendshipService friendshipService)
        {
            _userService = userService;
            _friendshipService = friendshipService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers([FromQuery]PaginationQuery paginationQuery)
        {
            var paginationResult = await _userService.GetUsers(paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUser(string id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInformation(string id,
            [FromBody] UserForUpdateDto userForUpdate)
        {
            if (id != HttpContext.GetUserId())
            {
                BadRequest();
            }

            await _userService.UpdateUserInformation(id, userForUpdate);

            return NoContent();
        }

        [HttpGet("{id}/friends")]
        public async Task<IEnumerable<FriendshipForListDto>> GetUserFriends(string id,
            [FromQuery]PaginationQuery paginationQuery)
        {
            var paginationResult = await _friendshipService.GetUserFriends(id, paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }
    }
}
