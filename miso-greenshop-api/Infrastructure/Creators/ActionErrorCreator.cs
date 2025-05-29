﻿using miso_greenshop_api.Domain.Interfaces.Creators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace miso_greenshop_api.Infrastructure.Creators
{
    public class ActionErrorCreator : IActionErrorCreator
    {
        public void CreateActionError<TResult>(
            ActionExecutingContext context, 
            string key, 
            string message, 
            int statusCode, 
            Func<ValidationProblemDetails, TResult> resultFactory) 
            where TResult : 
            ObjectResult
        {
            context.ModelState
                .AddModelError(
                key, 
                message);

            var problemDetails = 
            new ValidationProblemDetails(context.ModelState)
            {
                Status = statusCode
            };

            context.Result = resultFactory(problemDetails);
        }
    }
}
