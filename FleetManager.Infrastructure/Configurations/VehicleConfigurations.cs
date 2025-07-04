using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FleetManager.Infrastructure.Configurations
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => new { v.ChassisSeries, v.ChassisNumber });

            builder.Property(v => v.ChassisSeries)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.ChassisNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder
            .HasDiscriminator<VehicleType>("VehicleType")
                .HasValue<Vehicle>(VehicleType.Unknown)
                .HasValue<Car>(VehicleType.Car)
                .HasValue<Truck>(VehicleType.Truck)
                .HasValue<Bus>(VehicleType.Bus);
        }
    }
}
