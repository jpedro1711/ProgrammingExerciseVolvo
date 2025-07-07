using FleetManager.Application.Interfaces.Repositories;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Owned;
using FleetManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.Repositories
{
    public class VehicleRepository(FleetManagerDatabaseContext _context) : IVehicleRepository
    {
        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetByChassisId(ChassisId chassisId)
        {
            return await _context.Vehicles.SingleOrDefaultAsync(v => v.ChassisSeries == chassisId.ChassisSeries && v.ChassisNumber == chassisId.ChassisNumber);
        }

        public async Task<Vehicle> Insert(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> Update(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
    }
}
