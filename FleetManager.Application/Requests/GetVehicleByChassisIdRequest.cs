using FleetManager.Domain.Entities.Owned;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Application.Requests
{
    public class GetVehicleByChassisIdRequest
    {
        [Required(ErrorMessage = "ChassisId is required.")]
        public required ChassisId ChassisId { get; set; }
    }
}
