using System;
using System.Collections.Generic;
using AutoMapper;
using SocialNetwork.BLL.DTOs.Conversation;
using SocialNetwork.BLL.DTOs.Participant;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.BLL.Helpers
{
    public class ConversationsAutoMapperProfile : Profile
    {
        public ConversationsAutoMapperProfile()
        {
            CreateMap<Conversation, ConversationForListDto>();

            CreateMap<ConversationForCreationDto, Conversation>()
                .ForMember(c => c.Participants, opt => opt.MapFrom(src => new List<Participant>()
                {
                    new Participant() {UserId = src.FirstUserId},
                    new Participant() {UserId = src.SecondUserId}
                }));


            CreateMap<Participant, ParticipantDto>();
        }
    }
}
