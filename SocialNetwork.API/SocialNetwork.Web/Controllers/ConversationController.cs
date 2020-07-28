using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.DTOs.Participant;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Filters;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
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

            var conversation = await _conversationService.CreateConversation(userId, conversationForCreationDto);

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

        [HttpPut("{id}/read")]
        public async Task<IActionResult> ReadConversation(string id)
        {
            var userId = HttpContext.GetUserId();

            await _conversationService.MarkConversationAsRead(userId, id);

            return NoContent();
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

        [HttpGet("{id}/participants")]
        public async Task<IEnumerable<ParticipantDto>> GetConversationParticipants(string id,
            [FromQuery]PaginationQuery paginationQuery)
        {
            var userId = HttpContext.GetUserId();

            var paginationResult = await _conversationService.GetConversationParticipants(userId, id, paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }
    }
}
