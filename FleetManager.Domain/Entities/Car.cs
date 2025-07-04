using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Car(string chassisSeries, int chassisNumber, string color) : Vehicle(chassisSeries, chassisNumber, color)
    {
        private const int CarNumberOfPassengers = 4;

        public override VehicleType Type { get; set; } = VehicleType.Car;
        public override int NumberOfPassengers { get; set; } = CarNumberOfPassengers;
    }
}
