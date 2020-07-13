using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Filters;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserActivityActionFilter))]
    [Route("api/friends")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet]
        public async Task<IEnumerable<FriendshipForListDto>> GetFriendships([FromQuery]PaginationQuery paginationQuery)
        {
            var id = HttpContext.GetUserId();

            var paginationResult = await _friendshipService.GetUserFriends(id, paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

        [HttpGet("requests")]
        public async Task<IEnumerable<FriendshipForListDto>> GetFriendshipRequests([FromQuery]PaginationQuery paginationQuery)
        {
            var id = HttpContext.GetUserId();

            var paginationResult = await _friendshipService.GetUserFriendshipsRequests(id, paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriendshipRequest([FromBody]FriendshipDto friendshipDto)
        {
            var id = HttpContext.GetUserId();

            if (id != friendshipDto.SenderId)
            {
                throw new InvalidUserIdException(friendshipDto.SenderId);
            }

            await _friendshipService.AddFriendshipRequest(friendshipDto);

            return NoContent();
        }

        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptFriendshipRequest(string id)
        {
            var userId = HttpContext.GetUserId();

            await _friendshipService.AcceptFriendshipRequest(userId, id);

            return Ok();
        }

        [HttpPut("{id}/decline")]
        public async Task<IActionResult> DeclineFriendshipRequest(string id)
        {
            var userId = HttpContext.GetUserId();

            await _friendshipService.DeclineFriendshipRequest(userId, id);

            return Ok();
        }
    }
}
