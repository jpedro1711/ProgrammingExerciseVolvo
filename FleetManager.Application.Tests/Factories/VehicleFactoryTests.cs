using FleetManager.Application.Factories;
using FleetManager.Application.Requests;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace FleetManager.Application.Tests.Factories
{
    public class VehicleFactoryTests
    {
        private readonly VehicleFactory _factory;

        public VehicleFactoryTests()
        {
            _factory = new VehicleFactory();
        }

        [Fact]
        public void CreateVehicle_ReturnsBus_WhenVehicleTypeIsBus()
        {
            var request = new CreateVehicleRequest
            {
                VehicleType = VehicleType.Bus,
                ChassisId = new ChassisId("BUS123", 1),
                Color = "Blue"
            };

            var result = _factory.CreateVehicle(request);

            Assert.IsType<Bus>(result);
            Assert.Equal(request.ChassisId.ChassisSeries, result.ChassisId.ChassisSeries);
            Assert.Equal(request.Color, result.Color);
        }

        [Fact]
        public void CreateVehicle_ReturnsTruck_WhenVehicleTypeIsTruck()
        {
            var request = new CreateVehicleRequest
            {
                VehicleType = VehicleType.Truck,
                ChassisId = new ChassisId("TRK456", 2),
                Color = "Red"
            };

            var result = _factory.CreateVehicle(request);

            Assert.IsType<Truck>(result);
            Assert.Equal(request.ChassisId.ChassisSeries, result.ChassisId.ChassisSeries);
            Assert.Equal(request.Color, result.Color);
        }

        [Fact]
        public void CreateVehicle_ReturnsCar_WhenVehicleTypeIsCar()
        {
            var request = new CreateVehicleRequest
            {
                VehicleType = VehicleType.Car,
                ChassisId = new ChassisId("CAR789", 3),
                Color = "Green"
            };

            var result = _factory.CreateVehicle(request);

            Assert.IsType<Car>(result);
            Assert.Equal(request.ChassisId.ChassisSeries, result.ChassisId.ChassisSeries);
            Assert.Equal(request.Color, result.Color);
        }

        [Fact]
        public void CreateVehicle_ThrowsArgumentException_WhenVehicleTypeIsInvalid()
        {
            var request = new CreateVehicleRequest
            {
                VehicleType = (VehicleType)999,
                ChassisId = new ChassisId("INV000", 0),
                Color = "Black"
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateVehicle(request));
        }
    }
}
