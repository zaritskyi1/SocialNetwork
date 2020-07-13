using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Security;
using SocialNetwork.BLL.Services.Interfaces;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenSettings _tokenSettings;
        private readonly IMapper _mapper;


        public AuthService(UserManager<User> userManager,
            IOptions<TokenSettings> tokenSettings, 
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
            _mapper = mapper;
        }

        public async Task<string> LogIn(UserForLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            if (user == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            if (!result)
            {
                return null;
            }

            var token = await GenerateUserToken(user);

            return token;
        }

        public async Task<UserDto> RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            var user = _mapper.Map<User>(userForRegisterDto);

            var createUserResult  = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (createUserResult.Succeeded)
            {
                var addRoleToUserResult = await _userManager.AddToRoleAsync(user, "User");

                if (addRoleToUserResult.Succeeded)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return userDto;
                }

                throw new IdentityException(addRoleToUserResult.Errors);
            }

            throw new IdentityException(createUserResult.Errors);
        }

        private async Task<string> GenerateUserToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretBytes = Encoding.UTF8.GetBytes(_tokenSettings.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
