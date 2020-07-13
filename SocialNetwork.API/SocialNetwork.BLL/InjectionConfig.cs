using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.BLL.MappingProfiles;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.BLL
{
    public static class InjectionConfig
    {
        public static void ConfigureBllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDalServices(configuration);

            services.AddIdentityCore<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SocialNetworkContext>();
                //.AddSignInManager<SignInManager<User>>();

            services.AddAutoMapper(typeof(BLLToDALMapperProfile), 
                typeof(DALToBLLMapperProfile));

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            services.AddTransient<IConversationService, ConversationService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IReportService, ReportService>();

            services.AddTransient<ISeedingDataService, SeedingDataService>();
        }
    }
}
