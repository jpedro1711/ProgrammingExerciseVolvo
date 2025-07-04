using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public abstract class Vehicle
    {
        public string ChassisSeries { get; }
        public int ChassisNumber { get; }
        public string Color { get; set; }
        public abstract int NumberOfPassengers { get; set; }
        public abstract VehicleType Type { get; set; }

        protected Vehicle(string chassisSeries, int chassisNumber, string color)
        {
            ChassisSeries = chassisSeries;
            ChassisNumber = chassisNumber;
            Color = color;
        }

        public ChassisId ChassisId => new ChassisId
        (
            ChassisSeries,
            ChassisNumber
        );
    }
}
