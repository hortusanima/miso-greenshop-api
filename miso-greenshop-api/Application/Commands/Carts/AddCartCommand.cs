using miso_greenshop_api.Dtos.CartItems;
using miso_greenshop_api.Dtos.Carts;
using MediatR;

namespace miso_greenshop_api.Application.Commands.Carts
{
    public class AddCartCommand : IRequest<CartDto>
    {
        public List<CartItemDto>? CartItems { get; set; }
    }
}
