using Microsoft.AspNetCore.Mvc;
using AutoMob_WebAPI.Models;
using AutoMob_WebAPI.Repository;


namespace AutoMob_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repository;

        public VehicleController(IVehicleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAllVehicles")]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _repository.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("GetAllVehicles/{id}")]
        public IActionResult GetVehiclesById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Please provide a valid vehicle id");
            }
            var vehicles = _repository.GetVehicleById(id);
            return Ok(vehicles);
        }

        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle(VehicleModel vehicle)
        {
            _repository.AddVehicle(vehicle);
            return Ok("Vehicle added successfully");
        }

        [HttpPut("UpdateVehicle")]
        public IActionResult UpdateVehicle(VehicleModel vehicle)
        {
            _repository.UpdateVehicle(vehicle);
            return Ok("Vehicle updated successfully");
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please provide a valid vehicle id");
            }
            _repository.DeleteVehicle(id);
            return Ok("Vehicle deleted successfully");
        }
    }
}
