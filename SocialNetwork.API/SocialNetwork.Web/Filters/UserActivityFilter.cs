using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Extensions;

namespace SocialNetwork.Web.Filters
{
    public class UserActivityActionFilter : IAsyncActionFilter
    {
        private readonly IUserService _userService;

        public UserActivityActionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            var userId = context.HttpContext.GetUserId();

            await _userService.UpdateUserActivity(userId);
        }
    }
}
