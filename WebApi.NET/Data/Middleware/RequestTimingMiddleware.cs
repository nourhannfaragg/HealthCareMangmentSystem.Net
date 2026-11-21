using System.Diagnostics;

namespace WebApi.NET.Data.Middleware
{
    public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            Exception? failure = null;

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                failure = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();

                if (failure is null)
                {
                    logger.LogInformation(
                        "HTTP {Method} {Path} returned {StatusCode} in {ElapsedMilliseconds} ms",
                        context.Request.Method,
                        context.Request.Path,
                        context.Response.StatusCode,
                        stopwatch.Elapsed.TotalMilliseconds
                        );
                }
                else
                {
                    logger.LogError(
                        "HTTP {Method} {Path} failed after {ElapsedMilliseconds} ms",
                        context.Request.Method,
                        context.Request.Path,
                        stopwatch.Elapsed.TotalMilliseconds
                        );
                }
            }
        }
    }
}
