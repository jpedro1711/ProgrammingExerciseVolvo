using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Truck(string chassisSeries, int chassisNumber, string color) : Vehicle(chassisSeries, chassisNumber, color)
    {
        private const int TruckNumberOfPassengers = 1;

        public override VehicleType Type { get; set; } = VehicleType.Truck;
        public override int NumberOfPassengers { get; set; } = TruckNumberOfPassengers;
    }
}
