using AutoMob_WebAPI.Controllers;
using AutoMob_WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace AutoMob_WebAPI.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext _context;
        private readonly ILogger<VehicleController> _logger;

        public VehicleRepository(VehicleDbContext context, ILogger<VehicleController> logger)
        { 
            _context = context;
            _logger = logger;
        }

        public IEnumerable<VehicleModel> GetAllVehicles()
        {
            try
            {
                _logger.LogInformation("Received request to retrieve all vehicles.");
                return _context.Vehicles.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all vehicles.");
                throw;
            }
        }

        public VehicleModel GetVehicleById(int vehicleId)
        {
            try
            {

                return _context.Vehicles.Find(vehicleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the vehicle with ID {VehicleId}.", vehicleId);
                throw new Exception("An error occurred while retrieving vehicles.", ex);
            }
        }

        public void AddVehicle(VehicleModel vehicle)
        {
            try
            {
                vehicle.Id = 0; // Id is an Identity column, Ensure EF Core does not treat it as an explicit value
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the vehicle.");
                throw new Exception("An error occurred while retrieving the vehicle.", ex);
            }
        }

        public bool UpdateVehicle(VehicleModel vehicle)
        {
            try
            {
                _logger.LogInformation("Updating vehicle with ID {VehicleId} in the database.", vehicle.Id);
                if (VehicleExists(vehicle.Id))  //Find if the vehicle exists, if exists, update
                {
                    _context.Vehicles.Update(vehicle);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the vehicle with ID {VehicleId}.", vehicle.Id);
                throw new Exception("An error occurred while adding the vehicle.", ex);
            }
        }

        public bool PatchVehicle(int vehicleId, JsonPatchDocument<VehicleModel> patchDoc)
        {
            try
            {

                var vehicle = _context.Vehicles.FirstOrDefault(x => x.Id == vehicleId);
                if (vehicle == null)
                {
                    return false;
                }
                patchDoc.ApplyTo(vehicle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while patching the vehicle with ID {VehicleId}.", vehicleId);
                throw new Exception("An error occurred while patching the vehicle.", ex);
            }
        }

        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                var vehicle = _context.Vehicles.Find(vehicleId);
                if (vehicle != null)
                {
                    _context.Vehicles.Remove(vehicle);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the vehicle with ID {VehicleId}.", vehicleId);
                throw new Exception("An error occurred while deleting the vehicle.", ex);
            }
        }

        private bool VehicleExists(int vehicleId)
        {
            _logger.LogInformation("Checking if vehicle with ID {VehicleId} exists in the database.", vehicleId);
            return _context.Vehicles.Any(x => x.Id == vehicleId);
        }
    }
}
