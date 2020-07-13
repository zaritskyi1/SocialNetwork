using System;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Friendship;
using SocialNetwork.BLL.DTOs.Message;
using SocialNetwork.BLL.DTOs.User;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.BLL.MappingProfiles
{
    public class BLLToDALMapperProfile : Profile
    {
        public BLLToDALMapperProfile()
        {
            ConfigureUserMapping();
            ConfigureMessageMapping();
            ConfigureFriendshipMapping();
            ConfigurePaginationQueryMapping();
        }

        private void ConfigureUserMapping()
        {
            CreateMap<UserForLoginDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.UserName.ToString()));


            CreateMap<UserForRegisterDto, User>()
                .ForMember(u => u.LastActive, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(u => u.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.UserName.ToLower()));

            CreateMap<UserForUpdateDto, User>();
        }

        private void ConfigureMessageMapping()
        {
            CreateMap<MessageForCreation, Message>();
        }

        private void ConfigureFriendshipMapping()
        {
            CreateMap<FriendshipDto, Friendship>();
        }

        private void ConfigurePaginationQueryMapping()
        {
            CreateMap<PaginationQuery, QueryOptions>();
        }
    }
}
