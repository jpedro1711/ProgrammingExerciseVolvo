using FleetManager.Domain.Constants;
using FleetManager.Domain.Enums;

namespace FleetManager.Domain.Entities
{
    public class Car(string chassisSeries, uint chassisNumber, string color) : Vehicle(chassisSeries, chassisNumber, color)
    {
        public override VehicleType Type { get; } = VehicleType.Car;
        public override int NumberOfPassengers { get; } = VehicleNumberOfPassengers.CarNumberOfPassengers;
    }
}
