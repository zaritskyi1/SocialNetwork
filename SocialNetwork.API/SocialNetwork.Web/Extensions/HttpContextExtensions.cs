using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                throw new EntityNotFoundException("Can't find user.", typeof(HttpContext));
            }

            return httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
