using FleetManager.Domain.Constants;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Bus(string chassisSeries, uint chassisNumber, string color) : Vehicle(chassisSeries, chassisNumber, color)
    {
        public override VehicleType Type { get; } = VehicleType.Bus;
        public override int NumberOfPassengers { get; } = VehicleNumberOfPassengers.BusNumberOfPassengers;
    }
}
