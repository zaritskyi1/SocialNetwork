using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNetwork.BLL;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.Web.Converters;
using SocialNetwork.Web.Filters;
using SocialNetwork.Web.Middleware;

namespace SocialNetwork.Web
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureBllServices(Configuration);

            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.AddCors();

            services.AddTransient<UserActivityActionFilter>();

            ConfigureAuthentication(services);

            ConfigureAuthorization(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedingDataService seedingDataService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }

            seedingDataService.SeedData();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
