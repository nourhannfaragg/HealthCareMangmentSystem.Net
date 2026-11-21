namespace WebApi.NET.Data.Middleware
{
    public class RequestIdHeaderMiddleware(ILogger<RequestIdHeaderMiddleware> logger):IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestId = context.TraceIdentifier;
            context.Response.Headers["X-Request-Id"] = requestId;

            logger.LogDebug(
                "assigned request ID {RequestId} to {Method} {Path}",
                requestId,
                context.Request.Method,
                context.Request.Path
                );

            await next(context);
        }
    }
}
