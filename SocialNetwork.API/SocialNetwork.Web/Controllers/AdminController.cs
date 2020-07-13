using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.Role;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;

namespace SocialNetwork.Web.Controllers
{
    [ApiController]
    [Route("api/admin/")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users")]
        public async Task<IEnumerable<UserWithRoleDto>> GetUsersWithRoles([FromQuery]PaginationQuery paginationQuery)
        {
            var paginationResult = await _adminService.GetUsersWithRoles(paginationQuery);

            Response.AddPagination(paginationResult.Information);

            return paginationResult.Result;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateUserRole(string id, RoleEditDto editDto)
        {
            await _adminService.EditUserRoles(id, editDto);

            return NoContent();
        }
    }
}
