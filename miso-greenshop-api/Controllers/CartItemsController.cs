using miso_greenshop_api.Application.Commands.CartItems;
using miso_greenshop_api.Dtos.CartItems;
using miso_greenshop_api.Filters.ActionFilters.Cart_ActionFilters;
using miso_greenshop_api.Filters.ActionFilters.CartItems_ActionFilters;
using miso_greenshop_api.Filters.ActionFilters.General;
using miso_greenshop_api.Filters.ActionFilters.Plant_ActionFilters;
using miso_greenshop_api.Filters.ActionFilters.User_ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace miso_greenshop_api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CartItemsController(IMediator mediator) : 
        ControllerBase
    {
        private readonly IMediator _mediator = 
            mediator;

        [HttpPost]
        [EnableCors("WithCredentialsPolicy")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        [TypeFilter(typeof(Cart_ValidateCartExistsActionFilter))]
        [TypeFilter(typeof(CartItem_ValidatePlantIdActionFilter))]
        public async Task<IActionResult> CreateCartItem([FromBody]CartItemDto cartItem)
        {
            await _mediator.Send(
            new AddCardItemCommand
            {
                CartItem = cartItem
            });

            return NoContent();
        }

        [HttpDelete("{plantId}")]
        [EnableCors("WithCredentialsPolicy")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        [TypeFilter(typeof(Plant_ValidatePlantIdActionFilter))]
        [TypeFilter(typeof(Cart_ValidateCartExistsActionFilter))]
        [TypeFilter(typeof(CartItem_ValidateCartItemExistsActionFIlter))]
        public async Task<IActionResult> DeleteCartItem([FromRoute]string plantId)
        {
            await _mediator.Send(
            new DeleteCartItemCommand
            {
                PlantId = plantId
            });

            return NoContent();
        }
    }
}
