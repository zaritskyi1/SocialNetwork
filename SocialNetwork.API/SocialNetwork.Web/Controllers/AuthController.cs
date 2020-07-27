using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Services.Interfaces;

namespace SocialNetwork.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var token = await _authService.LogIn(userForLoginDto);

            return Ok(new
            {
                token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var createdUser = await _authService.RegisterUser(userForRegisterDto);

            if (createdUser == null)
            {
                return BadRequest("Can`t create user!");
            }

            return CreatedAtAction("GetUser", 
                new { controller = "User", id = createdUser.Id }, createdUser);
        }
    }
}
