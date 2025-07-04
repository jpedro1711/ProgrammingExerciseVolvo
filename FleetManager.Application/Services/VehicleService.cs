using FleetManager.Application.Factories.Interfaces;
using FleetManager.Application.Interfaces.Repositories;
using FleetManager.Application.Interfaces.Services;
using FleetManager.Application.Requests;
using FleetManager.Application.Resources;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Services
{
    public class VehicleService(IVehicleRepository _vehicleRepository, IVehicleFactory _vehicleFactory) : IVehicleService
    {
        public async Task<Vehicle> Insert(CreateVehicleRequest createVehicleRequest)
        {
            Vehicle? vehicleExists = await GetByChassisId(new GetVehicleByChassisIdRequest { ChassisId = createVehicleRequest.ChassisId });

            if (vehicleExists is not null)
                throw new ArgumentException(string.Format(ResponseMessages.VehicleAlreadyExistsMessage, createVehicleRequest.ChassisId.ToString()));

            Vehicle vehicle = _vehicleFactory.CreateVehicle(createVehicleRequest);
            return await _vehicleRepository.Insert(vehicle);
        }

        public async Task<Vehicle?> GetByChassisId(GetVehicleByChassisIdRequest request)
        {
            return await _vehicleRepository.GetByChassisId(request.ChassisId);
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _vehicleRepository.GetAll();
        }

        public async Task<Vehicle> Update(EditVehicleRequest editVehicleRequest, ChassisId chassisId)
        {
            Vehicle? existingVehicle = await GetByChassisId(new GetVehicleByChassisIdRequest { ChassisId = chassisId }) ??
                            throw new KeyNotFoundException(string.Format(ResponseMessages.VehicleNotFoundMessage, chassisId.ToString()));

            existingVehicle.Color = editVehicleRequest.Color;

            return await _vehicleRepository.Update(existingVehicle);
        }
    }
}
