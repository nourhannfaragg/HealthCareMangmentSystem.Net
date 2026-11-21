using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.NET.Filters
{
    public class ActionExecutionLoggingFilter(ILogger<ActionExecutionLoggingFilter> logger) : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

            logger.LogInformation("Starting MVC action {ActionName}", context.ActionDescriptor.DisplayName);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            if (context.Exception is not null && !context.ExceptionHandled)
            {
                logger.LogError(context.Exception, "MVC action {ActionName} failed", actionName);
                return;
            }

            logger.LogInformation("Finishing MVC action {ActionName}", actionName);
        }
    }
}
