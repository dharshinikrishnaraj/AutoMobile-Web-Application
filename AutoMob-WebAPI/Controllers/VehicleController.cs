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
            try
            {
                var vehicles = _repository.GetAllVehicles();
                return Ok(vehicles);
            }
            catch (Exception)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllVehicles/{id}")]
        public IActionResult GetVehiclesById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid vehicle id");
                }
                var vehicles = _repository.GetVehicleById(id);
                return Ok(vehicles);
            }
            catch (Exception)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle(VehicleModel vehicle)
        {
            try
            {
                _repository.AddVehicle(vehicle);
                return Ok("Vehicle added successfully");
            }
            catch (Exception)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateVehicle")]
        public IActionResult UpdateVehicle(VehicleModel vehicle)
        {
            if(vehicle.Id <= 0)
            {
                return BadRequest("Please provide a valid vehicle id");
            }
            try
            {
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
            catch (Exception)
            { 
                 // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please provide a valid vehicle id");
            }
            try
            {
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
            catch(Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
