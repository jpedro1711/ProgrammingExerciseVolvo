﻿using FleetManager.Application.Requests;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Owned;

namespace FleetManager.Application.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> Insert(CreateVehicleRequest createVehicleRequest);
        Task<Vehicle?> GetByChassisId(GetVehicleByChassisIdRequest request);
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> Update(EditVehicleRequest editVehicleRequest, ChassisId chassisId);
    }
}
