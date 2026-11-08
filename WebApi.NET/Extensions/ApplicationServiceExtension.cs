using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Data.Interceptors;
using WebApi.NET.Helper;
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
            services.AddSwaggerGen();


            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cs"))
                .AddInterceptors(new SoftDeleteInterceptor() , new ModifiedAtInterceptor() , new CreatedAtInterceptor());
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DoctorProfile>();
                cfg.AddProfile<PatientProfile>();
            });

            //Repository
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IPatientService, PatientService>();


            return services;

        }
    }
}
