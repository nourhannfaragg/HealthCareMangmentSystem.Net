using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApi.NET.Filters
{
    public class ActionTimingFilter(ILogger<ActionTimingFilter> logger) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await next();
            }
            finally
            {
                stopwatch.Stop();

                logger.LogInformation(
                    "MVC action {ActionName} took {ElapsedMilliseconds} ms",
                    context.ActionDescriptor.DisplayName,
                    stopwatch.Elapsed.TotalMilliseconds);
            }
        }
    }
}
