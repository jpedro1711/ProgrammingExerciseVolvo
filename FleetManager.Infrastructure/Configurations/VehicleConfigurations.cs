using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.Configurations
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.OwnsOne(v => v.ChassisId, chassisId => chassisId.HasIndex(c => new { c.ChassisSeries, c.ChassisNumber }).IsUnique());

            builder.HasKey(v => new { v.ChassisSeries, v.ChassisNumber });

            builder
            .HasDiscriminator<VehicleType>("VehicleType")
                .HasValue<Vehicle>(VehicleType.Unknown)
                .HasValue<Car>(VehicleType.Car)
                .HasValue<Truck>(VehicleType.Truck)
                .HasValue<Bus>(VehicleType.Bus);

            builder.Ignore(v => v.ChassisId);
        }
    }
}
