using miso_greenshop_api.Dtos.CartItems;
using MediatR;

namespace miso_greenshop_api.Application.Commands.CartItems
{
    public class AddCardItemCommand : IRequest<Unit>
    {
        public CartItemDto? CartItem { get; set; }
    }
}
