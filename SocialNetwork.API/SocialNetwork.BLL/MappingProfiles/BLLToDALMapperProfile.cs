using System;
using System.Collections.Generic;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Conversation;
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
            ConfigureConversationMapping();
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
            CreateMap<FriendshipForCreationDto, Friendship>();
        }

        private void ConfigurePaginationQueryMapping()
        {
            CreateMap<PaginationQuery, QueryOptions>();
        }

        private void ConfigureConversationMapping()
        {
            CreateMap<ConversationForCreationDto, Conversation>()
                .ForMember(c => c.Participants, opt => opt.MapFrom(src => new List<Participant>()
                {
                    new Participant() {UserId = src.FirstUserId},
                    new Participant() {UserId = src.SecondUserId}
                }));
        }
    }
}
