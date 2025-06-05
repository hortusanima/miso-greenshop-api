﻿using miso_greenshop_api.Application.Commands.Users;
using miso_greenshop_api.Application.Models;
using miso_greenshop_api.Application.Queries.Users;
using miso_greenshop_api.Domain.Interfaces.Service;
using miso_greenshop_api.Dtos.Users;
using miso_greenshop_api.Filters.ActionFilters.General;
using miso_greenshop_api.Filters.ActionFilters.User_ActionFilters;
using miso_greenshop_api.Filters.ExceptionFilters.User_ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace miso_greenshop_api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsersController(
        INewsletterService newsletterService, 
        IMediator mediator) : 
        ControllerBase
    {
        private readonly INewsletterService newsletterService = 
            newsletterService;
        private readonly IMediator _mediator = 
            mediator;

        [HttpGet("all")]
        [EnableRateLimiting("SlidingWindowIpAddressLimiter")]
        [TypeFilter(typeof(ValidateAdminKeyActionFilter))]
        public async Task<IActionResult> GetUsers()
        {
            var getUserDtos = await _mediator.Send(
                new GetAllUsersQuery());

            return Ok(getUserDtos);
        }

        [HttpPost("register")]
        [EnableRateLimiting("SlidingWindowIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateRegisterUserActionFilter))]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            await _mediator.Send(
            new AddUserCommand
            {
                RegisterDto = registerDto
            });

            await this.newsletterService.SendNewsletterAsync(
                "registration",
                new NewsletterHeader
                {
                    Recipient = registerDto.Email,
                    Details = registerDto.Name
                }
            );

            return NoContent();
        }

        [HttpPost("login")]
        [EnableRateLimiting("TokenBucketIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateLoginUserActionFilter))]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var jwt = await _mediator.Send(
            new GetJwtQuery
            {
                LoginDto = loginDto
            });

            Response.Cookies.Append(
            "jwt", 
            jwt, 
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new
            {
                Jwt = jwt
            });
        }

        [HttpPost("logout")]
        [EnableRateLimiting("SlidingWindowIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        public async Task<IActionResult> Logout()
        {
            await Task.CompletedTask;
            Response.Cookies.Delete("jwt");

            return NoContent();
        }

        [HttpGet]
        [EnableRateLimiting("SlidingWindowIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        public async Task<IActionResult> GetUser()
        {
            var getUserDto = await _mediator.Send(
                new GetUserQuery());

            return Ok(getUserDto);
        }

        [HttpPut("{isSubscribed}")]
        [EnableRateLimiting("TokenBucketIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        [TypeFilter(typeof(User_HandleUpdateUserExceptionFilter))]
        public async Task<IActionResult> UpdateUserIsSubscribed([FromRoute]bool isSubscribed)
        {
            await _mediator.Send(
            new UpdateUserIsSubscribedCommand 
            { 
                IsSubscribed = isSubscribed 
            });

            return NoContent();
        }

        [HttpDelete]
        [EnableRateLimiting("TokenBucketIpAddressRestrictLimiter")]
        [TypeFilter(typeof(ValidateApplicationKeyActionFilter))]
        [TypeFilter(typeof(User_ValidateJwtTokenActionFilter))]
        public async Task<IActionResult> DeleteUser()
        {
            await _mediator.Send(
                new DeleteUserCommand());
            Response.Cookies.Delete("jwt");

            return NoContent();
        }
    }
}
