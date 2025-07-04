using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Vehicle
    {
        public string ChassisSeries { get; }
        public int ChassisNumber { get; }
        public string Color { get; set; }
        public virtual int NumberOfPassengers { get; set; }
        public virtual VehicleType Type { get; set; }

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
