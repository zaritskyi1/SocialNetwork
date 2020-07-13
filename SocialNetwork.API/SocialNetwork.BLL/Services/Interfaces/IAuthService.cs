using System.Threading.Tasks;
using SocialNetwork.BLL.DTOs.User;

namespace SocialNetwork.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LogIn(UserForLoginDto userLoginDto);
        Task<UserDto> RegisterUser(UserForRegisterDto userForRegisterDto);
    }
}
