using AutoMapper;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.DTOs.MessageReport;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.BLL.MappingProfiles
{
    public class DALToBLLMapperProfile : Profile
    {
        public DALToBLLMapperProfile()
        {
            ConfigureUserMapping();
            ConfigureMessageMapping();
            ConfigureMessageReportMapping();
            ConfigureFriendshipMapping();
            ConfigurePaginationMapping();
        }

        private void ConfigurePaginationMapping()
        {
            CreateMap<PagedListInfo, PaginationInfo>();

            CreateMap(typeof(PagedList<>), typeof(PaginationResult<>));
        }

        private void ConfigureUserMapping()
        {
            CreateMap<User, UserDto>();

            CreateMap<User, UserForListDto>();

            CreateMap<User, UserWithRoleDto>();
        }

        private void ConfigureMessageMapping()
        {
            CreateMap<Message, MessageDto>();
        }

        private void ConfigureMessageReportMapping()
        {
            CreateMap<MessageReport, MessageReportForList>();
        }

        private void ConfigureFriendshipMapping()
        {
            CreateMap<Friendship, FriendshipForListDto>();
        }
    }
}
