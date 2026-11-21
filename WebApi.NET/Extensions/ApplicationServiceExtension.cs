using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Globalization;
using System.Text;
using WebApi.NET.Data;
using WebApi.NET.Data.Interceptors;
using WebApi.NET.Data.Middleware;
using WebApi.NET.Filters;
using WebApi.NET.Helper;
using WebApi.NET.Models;
using WebApi.NET.Repositories;
using WebApi.NET.Repository;
using WebApi.NET.Services;

namespace WebApi.NET.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddOpenApi();
            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token. Swagger will add the Bearer prefix."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });


            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cs"))
                .AddInterceptors(new SoftDeleteInterceptor() , new ModifiedAtInterceptor() , new CreatedAtInterceptor());
            });

            services.AddDbContext<AuthenticationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AuthenticationConnection")));

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<PatientProfile>();
            });

            //Repository
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IPatientService, PatientService>();

            //Exeptions
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            //Filters
            services.AddScoped<ActionExecutionLoggingFilter>();
            services.AddScoped<ActionTimingFilter>();
            services.AddScoped<ExceptionHandlingFilter>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AuthenticationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            services.AddScoped<IJwtService, JwtService>();

            //Cors

            var corsOrigins = configuration.GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? Array.Empty<string>();

            const string corsPolicyName = "ConfigurationOrigins";

            services.AddCors(options =>
                options.AddPolicy(corsPolicyName, policy =>
                {
                    if (corsOrigins.Length > 0)
                        policy.WithOrigins(corsOrigins);

                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                })

            );

            return services;

        }
    }
}
