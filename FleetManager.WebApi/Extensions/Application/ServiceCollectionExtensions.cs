using FleetManager.Application.Factories;
using FleetManager.Application.Factories.Interfaces;
using FleetManager.Application.Interfaces.Repositories;
using FleetManager.Application.Interfaces.Services;
using FleetManager.Application.Services;
using FleetManager.Infrastructure.Database;
using FleetManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.WebApi.Extensions.WebApplication
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVehicleFactory, VehicleFactory>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleService, VehicleService>();

            services.AddDbContext<FleetManagerDatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
