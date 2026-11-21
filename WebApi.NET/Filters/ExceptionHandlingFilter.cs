using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.NET.Error;

namespace WebApi.NET.Filters
{
    public class ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger, IHostEnvironment environment) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
                return;

            logger.LogError(context.Exception,
                "Unhandled exception in MVC action {ActionName} for request {TraceId}",
                context.ActionDescriptor.DisplayName,
                context.HttpContext.TraceIdentifier);

            var problem = ExceptionProblemDetailsFactory.Create(context.Exception, context.HttpContext, environment);

            var result = new ObjectResult(problem)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            result.ContentTypes.Add("application/problem+json");

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
