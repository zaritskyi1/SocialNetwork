using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Filters;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserActivityActionFilter))]
    [Route("api/conversations")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation(ConversationForCreationDto conversationForCreationDto)
        {
            var userId = HttpContext.GetUserId();

            if (conversationForCreationDto.FirstUserId != userId)
            {
                throw new InvalidUserIdException(nameof(conversationForCreationDto.FirstUserId));
            }

            var existingConversation = await _conversationService.GetConversationByUsersId(conversationForCreationDto.FirstUserId,
                conversationForCreationDto.SecondUserId);

            if (existingConversation != null)
            {
                return new RedirectToActionResult(nameof(GetConversation), nameof(ConversationController),
                    new {id = existingConversation.Id});
            }

            var conversation = await _conversationService.CreateConversation(conversationForCreationDto);
            return CreatedAtAction(nameof(GetConversation), new {id = conversation.Id}, conversation);
        }

        [HttpGet]
        public async Task<IEnumerable<ConversationForListDto>> GetConversations(
            [FromQuery]PaginationQuery paginationQuery)
        {
            var id = HttpContext.GetUserId();

            var paginationResult = await _conversationService.GetPaginatedConversationsByUserId(id, paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

        [HttpGet("{id}")]
        public async Task<ConversationForListDto> GetConversation(string id)
        {
            var userId = HttpContext.GetUserId();

            var conversation = await _conversationService.GetConversationById(userId, id);

            return conversation;
        }

        [HttpGet("{id}/messages")]
        public async Task<IEnumerable<MessageDto>> GetConversationMessages(string id,
            [FromQuery]PaginationQuery paginationQuery)
        {
            var userId = HttpContext.GetUserId();

            var paginationResult = await _conversationService.GetConversationMessages(userId, id, paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

    }
}
