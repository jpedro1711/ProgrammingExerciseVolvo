using FleetManager.Domain.Entities.Owned;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public abstract class Vehicle
    {
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public ChassisId ChassisId => new(ChassisSeries, ChassisNumber);
        public string Color { get; set; }
        public abstract int NumberOfPassengers { get; }
        public abstract VehicleType Type { get; }

        protected Vehicle(string chassisSeries, uint chassisNumber, string color)
        {
            ChassisSeries = chassisSeries;
            ChassisNumber = chassisNumber;
            Color = color;
        }
    }
}
