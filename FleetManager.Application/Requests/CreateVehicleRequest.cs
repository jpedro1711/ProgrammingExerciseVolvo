using FleetManager.Domain.Entities.Owned;
using FleetManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Application.Requests
{
    public class CreateVehicleRequest
    {
        [Required(ErrorMessage = "ChassisId is required.")]
        public ChassisId ChassisId { get; set; } = default!;

        [Required(ErrorMessage = "VehicleType is required.")]
        [EnumDataType(typeof(VehicleType), ErrorMessage = "Invalid vehicle type.")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; } = string.Empty;
    }
}
