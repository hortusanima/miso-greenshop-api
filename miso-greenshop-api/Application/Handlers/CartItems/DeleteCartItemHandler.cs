using miso_greenshop_api.Application.Commands.CartItems;
using miso_greenshop_api.Domain.Interfaces.Jwt;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using MediatR;
using miso_greenshop_api.Dtos.Carts;
using AutoMapper;

namespace miso_greenshop_api.Application.Handlers.CartItems
{
    public class DeleteCartItemHandler(
        ICartsRepository cartsRepository,
        ICartItemsRepository cartItemsRepository,
        IPlantsRepository plantsRepository,
        IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper) : 
        IRequestHandler<DeleteCartItemCommand, CartDto>
    {
        private readonly ICartsRepository _cartsRepository = 
            cartsRepository;
        private readonly ICartItemsRepository _cartItemsRepository = 
            cartItemsRepository;
        private readonly IPlantsRepository _plantsRepository = 
            plantsRepository;
        private readonly IJwtService _jwtService = 
            jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor = 
            httpContextAccessor;
        private readonly IMapper _mapper = mapper;
        public async Task<CartDto> Handle(
            DeleteCartItemCommand request, 
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

            var cartItemToDelete = await _cartItemsRepository
                .GetCartItemByIdsAsync(
                cart!.CartId!, 
                request.PlantId!);

            await _cartItemsRepository
                .DeleteCartItemAsync(cartItemToDelete!);

            double totalCartPrice = 0;
            var addedCartItems = await _cartItemsRepository
                .GetCartItemsByCartAsync(cart.CartId!);

            var plantIds = addedCartItems
                .Select(ci => ci.PlantId)
                .ToList();

            var plants = await _plantsRepository
                .GetPlantsByIdsAsync(plantIds!);

            foreach (var cartItem in addedCartItems)
            {
                if (plants.TryGetValue(
                cartItem.PlantId!,
                out var plant))
                {
                    totalCartPrice += (double)plant.Price! *
                        cartItem.Quantity;
                }
            }
            if (addedCartItems.Count == 0)
            {
                totalCartPrice = 0;
            }

            var result = await _cartsRepository
                .UpdateCartPriceAsync(
                cart,
                totalCartPrice);

            return _mapper
                .Map<CartDto>(result);
        }
    }
}
