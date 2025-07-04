using FleetManager.Application.Requests;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Factories.Interfaces
{
    public interface IVehicleFactory
    {
        Vehicle CreateVehicle(CreateVehicleRequest request);
    }
}
