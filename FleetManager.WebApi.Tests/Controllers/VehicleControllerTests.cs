using AutoFixture;
using FleetManager.Application.Interfaces.Services;
using FleetManager.Application.Requests;
using FleetManager.Domain.Entities;
using FleetManager.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FleetManager.WebApi.Tests.Controllers
{
    public class VehicleControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IVehicleService> _vehicleServiceMock;
        private readonly VehicleController _controller;

        public VehicleControllerTests()
        {
            _fixture = new Fixture();
            _vehicleServiceMock = _fixture.Freeze<Mock<IVehicleService>>();
            _controller = new VehicleController(_vehicleServiceMock.Object);
        }

        [Fact]
        public async Task UpdateVehicle_ReturnsOk_WhenValidRequest()
        {
            var request = _fixture.Create<EditVehicleRequest>();
            var chassisSeries = _fixture.Create<string>();
            var chassisNumber = _fixture.Create<int>();
            var updatedVehicle = _fixture.Create<Truck>();
            _vehicleServiceMock.Setup(s => s.Update(request, It.IsAny<ChassisId>())).ReturnsAsync(updatedVehicle);

            var result = await _controller.UpdateVehicle(chassisSeries, chassisNumber, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedVehicle, okResult.Value);
        }

        [Fact]
        public async Task GetByChassisId_ReturnsOk_WhenVehicleExists()
        {
            var chassisSeries = _fixture.Create<string>();
            var chassisNumber = _fixture.Create<int>();
            var vehicle = _fixture.Create<Truck>();
            _vehicleServiceMock.Setup(s => s.GetByChassisId(It.IsAny<GetVehicleByChassisIdRequest>())).ReturnsAsync(vehicle);

            var result = await _controller.GetByChassisId(chassisSeries, chassisNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(vehicle, okResult.Value);
        }

        [Fact]
        public async Task GetByChassisId_ReturnsNotFound_WhenVehicleDoesNotExist()
        {
            var chassisSeries = _fixture.Create<string>();
            var chassisNumber = _fixture.Create<int>();
            _vehicleServiceMock.Setup(s => s.GetByChassisId(It.IsAny<GetVehicleByChassisIdRequest>())).ReturnsAsync((Truck)null);

            var result = await _controller.GetByChassisId(chassisSeries, chassisNumber);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("not found", notFound.Value.ToString());
        }

        [Fact]
        public async Task GetAllVehicles_ReturnsOk_WithListOfVehicles()
        {
            var vehicles = _fixture.CreateMany<Truck>(3);
            _vehicleServiceMock.Setup(s => s.GetAll()).ReturnsAsync(vehicles);

            var result = await _controller.GetAllVehicles();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(vehicles, okResult.Value);
        }
    }
}
