using FleetManager.Domain.Constants;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Truck(string chassisSeries, uint chassisNumber, string color) : Vehicle(chassisSeries, chassisNumber, color)
    {
        public override VehicleType Type { get; } = VehicleType.Truck;
        public override int NumberOfPassengers { get; } = VehicleNumberOfPassengers.TruckNumberOfPassengers;
    }
}
