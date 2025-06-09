using MediatR;
using miso_greenshop_api.Dtos.Carts;

namespace miso_greenshop_api.Application.Commands.CartItems
{
    public class DeleteCartItemCommand : IRequest<CartDto>
    {
        public string? PlantId { get; set; }
    }
}
