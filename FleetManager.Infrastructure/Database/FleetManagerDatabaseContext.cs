using FleetManager.Domain.Entities;
using FleetManager.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Database
{
    public class FleetManagerDatabaseContext : DbContext
    {
        public FleetManagerDatabaseContext(DbContextOptions<FleetManagerDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
