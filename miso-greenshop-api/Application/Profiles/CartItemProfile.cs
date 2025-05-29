using AutoMapper;
using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Dtos.CartItems;

namespace miso_greenshop_api.Application.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile() 
        {
            CreateMap<CartItemDto, CartItem>()
                .ForMember(dest => dest.CartId, opt => opt
                .MapFrom((src, dest, destMember, context) =>
                context.Items["CartId"]));

            CreateMap<CartItem, CartItemDto>(); 
        }
    }
}
