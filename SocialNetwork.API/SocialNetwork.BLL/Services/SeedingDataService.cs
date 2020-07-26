using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.BLL.Services
{
    public class SeedingDataService : ISeedingDataService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFriendshipService _friendshipService;
        private List<User> _users;

        public SeedingDataService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IFriendshipService friendshipService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _friendshipService = friendshipService;
        }

        public void SeedData()
        {
            SeedRoleData();
            SeedUserData();
        }

        private void SeedUserData()
        {
            if (!_userManager.Users.Any())
            {
                SeedRoleData();

                var userData = File.ReadAllText("../SocialNetwork.BLL/Helpers/SeedData/UserSeedData.json");
                _users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in _users)
                {
                    user.UserName = user.UserName.ToLower();
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }

                SeedUserFriendshipData();
            }
        }

        private void SeedRoleData()
        {
            if (!_roleManager.Roles.Any())
            {
                var roleData = File.ReadAllText("../SocialNetwork.BLL/Helpers/SeedData/RoleSeedData.json");
                var rolesNames = JsonConvert.DeserializeObject<List<string>>(roleData);

                foreach (var roleName in rolesNames)
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
                }
            }
        }

        private void SeedUserFriendshipData()
        {
            for (int i = 0; i < Convert.ToInt32(_users.Count / 2.0) - 1; i++)
            {
                for (int j = i + 1; j < _users.Count; j++)
                {
                    _friendshipService.CreateFriendshipRequest(_users[i].Id, new FriendshipForCreationDto()
                    {
                        SenderId = _users[i].Id,
                        ReceiverId = _users[j].Id
                    }).Wait();
                }
            }

            for (int i = 0; i < Convert.ToInt32(_users.Count / 2.0); i++)
            {
                var userId = _users[i].Id;

                var awaiter = _friendshipService.GetUserFriendshipsRequests(userId, new PaginationQuery()).GetAwaiter();
                var requests = awaiter.GetResult();

                foreach (var request in requests.Result)
                {
                    _friendshipService.AcceptFriendshipRequest(userId, request.Id).Wait();
                }
            }
        }
    }
}
