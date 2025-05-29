using miso_greenshop_api.Dtos.Carts;
using MediatR;

namespace miso_greenshop_api.Application.Commands.Carts
{
    public class RemoveCartItemsCommand : IRequest<CartDto>
    {
    }
}
