using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.MessageReport;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;

namespace SocialNetwork.Web.Controllers
{
    [Route("api/report/")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize(Policy = "RequireAdminModeratorUserRole")]
        [HttpPost("messages/{id}")]
        public async Task<IActionResult> ReportMessage(string id)
        {
            var userId = HttpContext.GetUserId();

            await _reportService.CreateMessageReport(userId, id);

            return Ok();
        }

        [Authorize(Policy = "RequireAdminModeratorRole")]
        [HttpGet("messages")]
        public async Task<IEnumerable<MessageReportDto>> GetReportedMessages([FromQuery]PaginationQuery paginationQuery)
        {
            var paginationResult = await _reportService.GetReportMessages(paginationQuery);

            HttpContext.Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

        [Authorize(Policy = "RequireAdminModeratorRole")]
        [HttpDelete("messages/{id}/decline")]
        public async Task<IActionResult> DeclineReportedMessage(string id)
        {
            await _reportService.RemoveMessageReport(id);
            return Ok();
        }

        [Authorize(Policy = "RequireAdminModeratorRole")]
        [HttpDelete("messages/{id}/accept")]
        public async Task<IActionResult> AcceptReportedMessage(string id)
        {
            await _reportService.AcceptMessageReport(id);
            return Ok();
        }
    }
}
