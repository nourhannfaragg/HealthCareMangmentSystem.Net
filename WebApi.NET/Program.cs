using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Data.Middleware;
using WebApi.NET.Extensions;
using WebApi.NET.Helper;

namespace WebApi.NET
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.UseExceptionHandler();
            app.UseMiddleware<RequestTimingMiddleware>();
            app.UseMiddleware<RequestIdHeaderMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();

            // Swagger Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}