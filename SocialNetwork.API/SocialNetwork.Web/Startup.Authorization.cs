using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Web
{
    public partial class Startup
    {
        public static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireAdminModeratorRole", policy => policy.RequireRole("Administrator", "Moderator"));
                options.AddPolicy("RequireAdminModeratorUserRole", policy => policy.RequireRole("Administrator", "Moderator", "User"));
            });
        }
    }
}
