using AutoMapper;
using miso_greenshop_api.Application.Commands.Carts;
using miso_greenshop_api.Domain.Interfaces.Jwt;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Dtos.Carts;
using MediatR;

namespace miso_greenshop_api.Application.Handlers.Carts
{
    public class RemoveCartItemsHandler(
        ICartsRepository cartsRepository,
        ICartItemsRepository cartItemsRepository,
        IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper) : 
        IRequestHandler<RemoveCartItemsCommand, CartDto>
    {
        private readonly ICartsRepository _cartsRepository = 
            cartsRepository;
        private readonly ICartItemsRepository _cartItemsRepository = 
            cartItemsRepository;
        private readonly IJwtService _jwtService = 
            jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor = 
            httpContextAccessor;
        private readonly IMapper _mapper = 
            mapper;

        public async Task<CartDto> Handle(
            RemoveCartItemsCommand request, 
            CancellationToken cancellationToken)
        {
            var jwt = _httpContextAccessor.HttpContext?
                .Request.Cookies["jwt"];
            var token = _jwtService
                .Verify(jwt!);
            var userId = token.Issuer
                .ToString();

            var cart = await _cartsRepository
                .GetCartByUserIdAsync(userId);

            await _cartItemsRepository
                .DeleteCartItemsAsync(cart!.CartItems!);
            var result = await _cartsRepository
                .UpdateCartPriceAsync(cart, 0);

            return _mapper
                .Map<CartDto>(result);
        }
    }
}
