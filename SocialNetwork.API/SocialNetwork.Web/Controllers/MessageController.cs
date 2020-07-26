using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Filters;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserActivityActionFilter))]
    [Route("api/messages/")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<CreatedAtActionResult> CreateMessage(MessageForCreationDto messageForCreationDto)
        {
            var userId = HttpContext.GetUserId();

            var messageDto = await _messageService.CreateMessage(userId, messageForCreationDto);

            return CreatedAtAction(nameof(GetMessage), 
                new { id = messageDto.Id }, messageDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            var userId = HttpContext.GetUserId();

            await _messageService.DeleteMessage(userId, id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetMessage(string id)
        {
            var userId = HttpContext.GetUserId();

            var messageDto = await _messageService.GetMessage(userId, id);

            return messageDto;
        }
    }
}
