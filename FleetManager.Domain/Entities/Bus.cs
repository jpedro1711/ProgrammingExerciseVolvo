using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Bus(string chassisSeries, int chassisNumber, string color) : Vehicle(chassisSeries, chassisNumber, color)
    {
        private const int BusNumberOfPassengers = 42;

        public override VehicleType Type { get; set; } = VehicleType.Bus;
        public override int NumberOfPassengers { get; set; } = BusNumberOfPassengers;
    }
}
