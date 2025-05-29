using AutoMapper;
using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Dtos.Carts;

namespace miso_greenshop_api.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.CartItems, opt => opt
                .MapFrom(src => src.CartItems));
        }
    }
}
