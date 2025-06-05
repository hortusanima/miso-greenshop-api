using miso_greenshop_api.Application.Commands.Carts;
using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Application.Queries.Users;
using miso_greenshop_api.Domain.Interfaces.Service;
using miso_greenshop_api.Dtos.CartItems;
using miso_greenshop_api.Filters.ActionFilters.Cart_ActionFilters;
using miso_greenshop_api.Filters.ActionFilters.General;
using miso_greenshop_api.Filters.ActionFilters.User_ActionFilters;
using miso_greenshop_api.Filters.ExceptionFilters.Carts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace miso_greenshop_api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CartsController(
        INewsletterService newsletterService,
        IMediator mediator) : 
        ControllerBase
    {
        private readonly INewsletterService _newsletterService = 
            newsletterService;
        private readonly IMediator _mediator = 
            mediator;

        [HttpPost]
        [EnableRateLimiting("TokenBucketIpAddressLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        [TypeFilter(typeof(Cart_ValidatePlantIdsActionFilter))]
        [TypeFilter(typeof(Cart_HandleUpdateCartExceptionFilter))]
        public async Task<IActionResult> SyncCart([FromBody]List<CartItemDto> cartItems)
        {
            var cartDto = await _mediator.Send(
            new AddCartCommand
            {
                CartItems = cartItems
            });

            return Ok(cartDto);
        }

        [HttpPut]
        [EnableRateLimiting("TokenBucketIpAddressLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        [TypeFilter(typeof(Cart_ValidateCartExistsActionFilter))]
        [TypeFilter(typeof(Cart_HandleUpdateCartExceptionFilter))]

        public async Task<IActionResult> PurchaseCart()
        {
            var cartDto = await _mediator.Send(
                new RemoveCartItemsCommand());
            var getUserDto = await _mediator.Send(
                new GetUserQuery());

            await _newsletterService.SendNewsletterAsync(
                "purchase",
                new NewsletterHeader
                {
                    Recipient = getUserDto.UserEmail,
                    Details = getUserDto.UserName
                }
            );

            return Ok(cartDto);
        }
    }
}
