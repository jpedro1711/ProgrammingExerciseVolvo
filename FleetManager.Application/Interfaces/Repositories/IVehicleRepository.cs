using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Owned;

namespace FleetManager.Application.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> Insert(Vehicle vehicle);
        Task<Vehicle?> GetByChassisId(ChassisId chassisId);
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> Update(Vehicle vehicle);
    }
}
