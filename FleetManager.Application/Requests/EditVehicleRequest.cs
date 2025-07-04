using FleetManager.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Application.Requests
{
    public class EditVehicleRequest
    {
        [Required(ErrorMessage = "Color is required.")]
        public required string Color { get; set; }
    }
}
