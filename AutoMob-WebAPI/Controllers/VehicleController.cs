using Microsoft.AspNetCore.Mvc;
using AutoMob_WebAPI.Models;
using AutoMob_WebAPI.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;


namespace AutoMob_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repository;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(IVehicleRepository repository, ILogger<VehicleController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetAllVehicles")]
        public IActionResult GetAllVehicles()
        {
                _logger.LogInformation("Received request to retrieve all vehicles.");
                var vehicles = _repository.GetAllVehicles();
                return Ok(vehicles);
        }

        [HttpGet("GetAllVehicles/{id}")]
        public IActionResult GetVehiclesById(int id)
        {
           
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid vehicle ID {VehicleId} received.", id);
                    return BadRequest("Please provide a valid vehicle id");
                }
                _logger.LogInformation("Received request to retrieve all vehicles by Id");
                var vehicles = _repository.GetVehicleById(id);
                return Ok(vehicles);
        }

        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle(VehicleModel vehicle)
        {
                _logger.LogInformation("Received request to add a new vehicle.");
                _repository.AddVehicle(vehicle);
                return Ok("Vehicle added successfully");
        }

        [HttpPut("UpdateVehicle")]
        public IActionResult UpdateVehicle(VehicleModel vehicle)
        {
            if(vehicle.Id <= 0)
            {
                _logger.LogWarning("Invalid vehicle ID {VehicleId} received for update.", vehicle.Id);
                return BadRequest("Please provide a valid vehicle id");
            }
            _logger.LogInformation("Received request to update vehicle with ID {VehicleId}.", vehicle.Id);
            bool isUpdated = _repository.UpdateVehicle(vehicle);
            if (isUpdated)
            {
                return Ok("Vehicle updated successfully");
            }
            else
            {
                return NotFound("Vehicle not found");
            }
        }

        [HttpPatch("PatchVehicle/{id}")]
        public IActionResult PatchVehicle(int id, [FromBody] JsonPatchDocument<VehicleModel> patchDoc)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid vehicle ID {VehicleId} received for patching.", id);
                return BadRequest("Please provide a valid vehicle id");
            }

            if (patchDoc == null)
            {
                _logger.LogWarning("Invalid patch document received for vehicle ID {VehicleId}.", id);
                return BadRequest("Invalid patch document");
            }

            _logger.LogInformation("Received request to patch vehicle with ID {VehicleId}.", id);
            bool isPatched = _repository.PatchVehicle(id, patchDoc);
            if (isPatched)
            {
                return Ok("Vehicle patched successfully");
            }
            else
            {
                return NotFound("Vehicle not found");
            }
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid vehicle ID {VehicleId} received for deletion.", id);
                return BadRequest("Please provide a valid vehicle id");
            }
            _logger.LogInformation("Received request to delete vehicle with ID {VehicleId}.", id);
            bool isDeleted = _repository.DeleteVehicle(id);
            if (isDeleted)
            {
                return Ok("Vehicle deleted successfully");
            }
            else
            {
                return NotFound("Vehicle not found");
            }
        }
    }
}
