using AutoMapper;
using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Dtos.Subscribers;
using miso_greenshop_api.Dtos.Users;

namespace miso_greenshop_api.Application.Profiles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<Subscriber, SubscriberDto>();

            CreateMap<SubscriberDto, Subscriber>()
                .ForMember(dest => dest.SubscriberId, opt => opt
                .MapFrom(src => Guid.NewGuid()
                .ToString()));

            CreateMap<User, Subscriber>()
                .ForMember(dest => dest.SubscriberId, opt => opt
                .MapFrom(src => Guid.NewGuid()
                .ToString()))
                .ForMember(dest => dest.SubscriberEmail, opt => opt
                .MapFrom(src => src.UserEmail));

            CreateMap<RegisterDto, Subscriber>()
                .ForMember(dest => dest.SubscriberId, opt => opt
                .MapFrom(src => Guid.NewGuid()
                .ToString()))
                .ForMember(dest => dest.SubscriberEmail, opt => opt
                .MapFrom(src => src.Email));
        }
    }
}
