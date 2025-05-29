﻿using miso_greenshop_api.Application.Commands.Subscribers;
using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Application.Queries.Subscribers;
using miso_greenshop_api.Domain.Interfaces.Service;
using miso_greenshop_api.Dtos.Subscribers;
using miso_greenshop_api.Filters.ActionFilters.General;
using miso_greenshop_api.Filters.ActionFilters.Subscriber_ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace miso_greenshop_api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SubscribersController(
        INewsletterService newsletterService, 
        IMediator mediator) : 
        ControllerBase
    {
        private readonly INewsletterService _newsletterService = 
            newsletterService;
        private readonly IMediator _mediator = 
            mediator;

        [HttpGet]
        [EnableRateLimiting("SlidingWindowIpAddressLimiter")]
        [TypeFilter(typeof(ValidateAdminKeyActionFilter))]
        public async Task<IActionResult> GetSubscribers()
        {
            var subscriberDtos = await _mediator.Send(
                new GetAllSubscribersQuery());
            return Ok(subscriberDtos);
        }

        [HttpPost]
        [EnableRateLimiting("SlidingWindowIpAddressLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(Subscriber_ValidateCreateSubscriberActionFilter))]
        public async Task<IActionResult> CreateSubscriber([FromBody]SubscriberDto subscriber)
        {
            await _mediator.Send(
            new AddSubscriberCommand
            {
                Subscriber = subscriber
            });

            await _newsletterService.SendNewsletterAsync(
                "subscription",
                new NewsletterHeader
                {
                    Recipient = subscriber.SubscriberEmail
                }
            );

            return NoContent();
        }

        [HttpDelete("{subscriberEmail}")]
        [EnableRateLimiting("SlidingWindowIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateAdminKeyActionFilter))]
        [TypeFilter(typeof(Subscriber_ValidateSubscriberEmailActionFilter))]
        public async Task<IActionResult> DeleteSubscriber([FromRoute]string subscriberEmail)
        {
            await _mediator.Send(
            new DeleteSubscriberCommand
            {
                Email = subscriberEmail
            });

            return NoContent();
        }
    }
}
