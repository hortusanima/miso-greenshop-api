﻿using miso_greenshop_api.Domain.Interfaces.Creators;
using miso_greenshop_api.Domain.Interfaces.Jwt;
using miso_greenshop_api.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace miso_greenshop_api.Filters.ExceptionFilters.Review_ExceptionFilters
{
    public class Review_HandleUpdateExceptionFilter(
        IReviewsRepository reviewsRepository, 
        IExceptionCreator exceptionCreator, 
        IJwtService jwtService) : 
        IAsyncExceptionFilter
    {
        private readonly IReviewsRepository _reviewsRepository = 
            reviewsRepository;
        private readonly IExceptionCreator _exceptionCreator = 
            exceptionCreator;
        private readonly IJwtService _jwtService = 
            jwtService;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var jwt = context.HttpContext.Request
                .Cookies["jwt"];
            var token = _jwtService
                .Verify(jwt!);
            var userId = token.Issuer
                .ToString();

            var plantId = context.RouteData.Values["plantId"] as string;

            if (await _reviewsRepository
                .GetReviewByIdsAsync(userId, plantId!) == null)
            {
                _exceptionCreator
                    .CreateException(
                    context,
                    "Review",
                    "Review does not exist anymore.",
                    404,
                    problemDetails => new NotFoundObjectResult(problemDetails));
            }
        }
    }
}
