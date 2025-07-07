using FleetManager.Domain.Entities;
using FleetManager.Infrastructure.Database;
using FleetManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using FleetManager.Domain.Entities.Owned;

namespace FleetManager.Infrastructure.Tests.Repositories
{
    public class VehicleRepositoryTests : IAsyncLifetime
    {
        private readonly MsSqlContainer _dbContainer;
        private FleetManagerDatabaseContext _dbContext;
        private VehicleRepository _repository;

        public VehicleRepositoryTests()
        {
            _dbContainer = new MsSqlBuilder()
                .WithPassword("Your_password123")
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var options = new DbContextOptionsBuilder<FleetManagerDatabaseContext>()
                .UseSqlServer(_dbContainer.GetConnectionString())
                .Options;

            _dbContext = new FleetManagerDatabaseContext(options);
            await _dbContext.Database.EnsureCreatedAsync();

            _repository = new VehicleRepository(_dbContext);
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }

        [Fact]
        public async Task Insert_ShouldAddVehicle()
        {
            var vehicle = new Car("a", 1, "blue");

            var result = await _repository.Insert(vehicle);

            Assert.NotNull(result);
            Assert.Equal("a", result.ChassisId.ChassisSeries);
            Assert.Equal(vehicle.ChassisId.ChassisNumber, result.ChassisId.ChassisNumber);
            Assert.Equal("blue", result.Color);

            var dbVehicle = await _dbContext.Vehicles
                .SingleOrDefaultAsync(v => v.ChassisSeries == "a" && v.ChassisNumber == 1);

            Assert.NotNull(dbVehicle);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllVehicles()
        {
            var vehicle1 = new Car("a", 1, "blue");
            var vehicle2 = new Truck("b", 1, "yellow");

            _dbContext.Vehicles.AddRange(vehicle1, vehicle2);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByChassisId_ShouldReturnCorrectVehicle()
        {
            var vehicle = new Car("a", 1, "blue");
            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();

            var chassisId = new ChassisId("a", 1);
            var result = await _repository.GetByChassisId(chassisId);

            Assert.NotNull(result);
            Assert.Equal(vehicle.ChassisId.ChassisSeries, result.ChassisId.ChassisSeries);
            Assert.Equal(vehicle.ChassisId.ChassisNumber, result.ChassisId.ChassisNumber);
        }

        [Fact]
        public async Task Update_ShouldModifyVehicle()
        {
            var vehicle = new Car("a", 1, "blue");
            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();

            vehicle.Color = "White";
            var updated = await _repository.Update(vehicle);

            Assert.Equal("White", updated.Color);

            var dbVehicle = await _dbContext.Vehicles
                .SingleOrDefaultAsync(v => v.ChassisSeries == "a" && v.ChassisNumber == 1);

            Assert.Equal("White", dbVehicle?.Color);
        }
    }
}
