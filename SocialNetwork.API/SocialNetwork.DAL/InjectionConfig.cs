using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.UnitOfWorks;

namespace SocialNetwork.DAL
{
    public static class InjectionConfig
    {
        public static void ConfigureDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<SocialNetworkContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
