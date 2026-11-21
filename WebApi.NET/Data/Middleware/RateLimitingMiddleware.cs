namespace WebApi.NET.Data.Middleware
{
    public class RateLimitingMiddleware(RequestDelegate next)
    {
        private static int _counter = 0;
        private static DateTime _lastRequestDate = DateTime.Now;

        public async Task InvokeAsync(HttpContext context)
        {
            _counter++;

            if (DateTime.Now.Subtract(_lastRequestDate).Seconds > 10)
            {
                _counter = 1;
                _lastRequestDate = DateTime.Now;
                await next(context);
            }
            else
            {
                if (_counter > 10)
                {
                    _lastRequestDate = DateTime.Now;
                    await context.Response.WriteAsync("Rate Limit Exceeded!");
                }
                else
                {
                    _lastRequestDate = DateTime.Now;
                    await next(context);
                }
            }
        }

    }
}
