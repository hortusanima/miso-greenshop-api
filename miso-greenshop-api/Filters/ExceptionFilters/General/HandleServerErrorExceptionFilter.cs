using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace miso_greenshop_api.Filters.ExceptionFilters.General
{
    public class HandleServerErrorExceptionFilter(ILogger<HandleServerErrorExceptionFilter> logger) : 
        IAsyncExceptionFilter
    {
        private readonly ILogger<HandleServerErrorExceptionFilter> _logger = 
            logger;

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError
            (
                context.Exception, 
                "An unhandled exception occurred."
            );
            var errorResponse = new
            {
                Error = "An error occurred while processing " +
                "your request. Please try again later."
            };
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }
}
