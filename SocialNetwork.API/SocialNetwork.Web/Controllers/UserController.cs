using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Filters;

namespace SocialNetwork.Web.Controllers
{
    [ServiceFilter(typeof(UserActivityActionFilter))]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFriendshipService _friendshipService;
        private readonly IConversationService _conversationService;

        public UserController(IUserService userService, 
            IFriendshipService friendshipService,
            IConversationService conversationService)
        {
            _userService = userService;
            _friendshipService = friendshipService;
            _conversationService = conversationService;
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
            await _userService.UpdateUserInformation(id, userForUpdate);

            return NoContent();
        }

        [HttpGet("{id}/friendship-status")]
        public async Task<FriendshipDto> GetUserFriendshipStatus(string id)
        {
            var userId = HttpContext.GetUserId();

            var friendship = await _friendshipService.GetFriendshipByUserIds(userId, id);

            return friendship;
        }

        [HttpGet("{id}/conversation")]
        public async Task<ConversationForListDto> GetUserConversation(string id)
        {
            var userId = HttpContext.GetUserId();

            var conversation = await _conversationService.GetConversationByUsersId(userId, id);

            return conversation;
        }
    }
}
