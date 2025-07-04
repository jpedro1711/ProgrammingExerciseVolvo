using FleetManager.Application.Factories.Interfaces;
using FleetManager.Application.Requests;
using FleetManager.Application.Resources;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Enums;

namespace FleetManager.Application.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public Vehicle CreateVehicle(CreateVehicleRequest request)
        {
            return request.VehicleType switch
            {
                VehicleType.Bus => new Bus(request.ChassisId.ChassisSeries, request.ChassisId.ChassisNumber, request.Color),
                VehicleType.Truck => new Truck(request.ChassisId.ChassisSeries, request.ChassisId.ChassisNumber, request.Color),
                VehicleType.Car => new Car(request.ChassisId.ChassisSeries, request.ChassisId.ChassisNumber, request.Color),
                _ => throw new ArgumentException(ResponseMessages.InvalidVehicleType)
            };
        }
    }
}
