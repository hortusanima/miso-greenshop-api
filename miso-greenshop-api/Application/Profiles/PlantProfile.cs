using AutoMapper;
using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Dtos.Plants;

namespace miso_greenshop_api.Application.Profiles
{
    public class PlantProfile : Profile
    {
        public PlantProfile()
        {
            CreateMap<Plant, GetPlantDto>();

            CreateMap<PostPlantDto, Plant>()
                .ForMember(dest => dest.PlantId, opt => opt
                .MapFrom(src => Guid.NewGuid()
                .ToString()));
        }
    }
}
