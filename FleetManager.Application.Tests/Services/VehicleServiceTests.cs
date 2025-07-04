using AutoFixture;
using FleetManager.Application.Factories.Interfaces;
using FleetManager.Application.Interfaces.Repositories;
using FleetManager.Application.Requests;
using FleetManager.Application.Services;
using FleetManager.Domain.Entities;
using Moq;

namespace FleetManager.Application.Tests.Services
{
    public class VehicleServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<IVehicleFactory> _vehicleFactoryMock;
        private readonly VehicleService _service;

        public VehicleServiceTests()
        {
            _fixture = new Fixture();
            _vehicleRepositoryMock = _fixture.Freeze<Mock<IVehicleRepository>>();
            _vehicleFactoryMock = _fixture.Freeze<Mock<IVehicleFactory>>();
            _service = new VehicleService(_vehicleRepositoryMock.Object, _vehicleFactoryMock.Object);
        }

        [Fact]
        public async Task Insert_ShouldThrow_WhenVehicleAlreadyExists()
        {
            var request = _fixture.Create<CreateVehicleRequest>();
            var existingVehicle = _fixture.Create<Truck>();
            _vehicleRepositoryMock
                .Setup(r => r.GetByChassisId(request.ChassisId))
                .ReturnsAsync(existingVehicle);

            await Assert.ThrowsAsync<ArgumentException>(() => _service.Insert(request));
        }

        [Fact]
        public async Task Insert_ShouldCreateAndInsertVehicle_WhenVehicleDoesNotExist()
        {
            var request = _fixture.Create<CreateVehicleRequest>();
            var newVehicle = _fixture.Create<Truck>();
            _vehicleRepositoryMock
                .Setup(r => r.GetByChassisId(request.ChassisId))
                .ReturnsAsync((Truck)null);
            _vehicleFactoryMock
                .Setup(f => f.CreateVehicle(request))
                .Returns(newVehicle);
            _vehicleRepositoryMock
                .Setup(r => r.Insert(newVehicle))
                .ReturnsAsync(newVehicle);

            var result = await _service.Insert(request);

            Assert.Equal(newVehicle, result);
            _vehicleFactoryMock.Verify(f => f.CreateVehicle(request), Times.Once);
            _vehicleRepositoryMock.Verify(r => r.Insert(newVehicle), Times.Once);
        }

        [Fact]
        public async Task GetByChassisId_ShouldReturnVehicle_WhenExists()
        {
            var chassisId = _fixture.Create<ChassisId>();
            var request = new GetVehicleByChassisIdRequest { ChassisId = chassisId };
            var vehicle = _fixture.Create<Truck>();
            _vehicleRepositoryMock
                .Setup(r => r.GetByChassisId(chassisId))
                .ReturnsAsync(vehicle);

            var result = await _service.GetByChassisId(request);

            Assert.Equal(vehicle, result);
        }

        [Fact]
        public async Task GetByChassisId_ShouldReturnNull_WhenNotExists()
        {
            var chassisId = _fixture.Create<ChassisId>();
            var request = new GetVehicleByChassisIdRequest { ChassisId = chassisId };
            _vehicleRepositoryMock
                .Setup(r => r.GetByChassisId(chassisId))
                .ReturnsAsync((Truck)null);

            var result = await _service.GetByChassisId(request);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllVehicles()
        {
            var vehicles = _fixture.CreateMany<Truck>(3);
            _vehicleRepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(vehicles);

            var result = await _service.GetAll();

            Assert.Equal(vehicles, result);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenVehicleDoesNotExist()
        {
            var chassisId = _fixture.Create<ChassisId>();
            var request = _fixture.Create<EditVehicleRequest>();
            _vehicleRepositoryMock
                .Setup(r => r.GetByChassisId(chassisId))
                .ReturnsAsync((Truck)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.Update(request, chassisId));
        }

        [Fact]
        public async Task Update_ShouldUpdateVehicleColor_WhenVehicleExists()
        {
            var chassisId = _fixture.Create<ChassisId>();
            var request = _fixture.Create<EditVehicleRequest>();
            var existingVehicle = _fixture.Build<Truck>()
                .With(v => v.Color, "OldColor")
                .Create();
            _vehicleRepositoryMock
                .Setup(r => r.GetByChassisId(chassisId))
                .ReturnsAsync(existingVehicle);
            _vehicleRepositoryMock
                .Setup(r => r.Update(existingVehicle))
                .ReturnsAsync(existingVehicle);

            var result = await _service.Update(request, chassisId);

            Assert.Equal(request.Color, result.Color);
            _vehicleRepositoryMock.Verify(r => r.Update(existingVehicle), Times.Once);
        }
    }
}
