using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
