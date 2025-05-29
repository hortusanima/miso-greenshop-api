using AutoMapper;
using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Dtos.Reviews;

namespace miso_greenshop_api.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, GetReviewDto>()
            .ForMember(dest => dest.UserName, opt => opt
            .MapFrom(src => src.User != null ? 
            src.User.UserName : 
            null));

            CreateMap<PostReviewDto, Review>()
                .ForMember(dest => dest.Creation_Date, opt => opt
                .MapFrom(src => DateTime
                .SpecifyKind(DateTime.Now, DateTimeKind.Utc)));
        }

    }
}
