using FleetManager.Application.Interfaces.Services;
using FleetManager.Application.Requests;
using FleetManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController(IVehicleService vehicleService) : ControllerBase
    {
        /// <summary>
        /// Creates a new vehicle.
        /// </summary>
        /// <param name="request">The vehicle data to create.</param>
        /// <returns>The created vehicle and its location.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            var vehicle = await vehicleService.Insert(request);
            var chassisId = new { request.ChassisId.ChassisSeries, request.ChassisId.ChassisNumber };

            return CreatedAtAction(nameof(GetByChassisId), chassisId, vehicle);
        }

        /// <summary>
        /// Updates an existing vehicle by chassis ID.
        /// </summary>
        /// <param name="chassisSeries">The chassis series of the vehicle.</param>
        /// <param name="chassisNumber">The chassis number of the vehicle.</param>
        /// <param name="request">The updated vehicle data.</param>
        /// <returns>The updated vehicle.</returns>
        [HttpPut("{chassisSeries}/{chassisNumber}")]
        public async Task<IActionResult> UpdateVehicle(string chassisSeries, int chassisNumber, [FromBody] EditVehicleRequest request)
        {
            var updatedVehicle = await vehicleService.Update(request, BuildChassisId(chassisSeries, chassisNumber));
            return Ok(updatedVehicle);
        }

        /// <summary>
        /// Retrieves a vehicle by its chassis ID.
        /// </summary>
        /// <param name="chassisSeries">The chassis series of the vehicle.</param>
        /// <param name="chassisNumber">The chassis number of the vehicle.</param>
        /// <returns>The vehicle matching the provided chassis ID, or 404 if not found.</returns>
        [HttpGet("{chassisSeries}/{chassisNumber}")]
        public async Task<IActionResult> GetByChassisId(string chassisSeries, int chassisNumber)
        {
            var vehicle = await vehicleService.GetByChassisId(new GetVehicleByChassisIdRequest
            {
                ChassisId = BuildChassisId(chassisSeries, chassisNumber)
            });

            return vehicle == null
                ? NotFound($"Vehicle with chassis ID {chassisSeries}{chassisNumber} not found.")
                : Ok(vehicle);
        }

        /// <summary>
        /// Retrieves all vehicles.
        /// </summary>
        /// <returns>A list of all registered vehicles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await vehicleService.GetAll();
            return Ok(vehicles);
        }

        private static ChassisId BuildChassisId(string chassisSeries, int chassisNumber) =>
            new(chassisSeries, chassisNumber);
    }
}
