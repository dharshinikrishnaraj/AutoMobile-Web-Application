using AutoMob_WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AutoMob_WebAPI.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext _context;

        public VehicleRepository(VehicleDbContext context)
        { 
            this._context = context;
        }

        public IEnumerable<VehicleModel> GetAllVehicles()
        {
            try
            {
                return _context.Vehicles.ToList();
            }
            catch (Exception)
            {
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
                throw new Exception("An error occurred while retrieving the vehicle.", ex);
            }
        }

        public bool UpdateVehicle(VehicleModel vehicle)
        {
            try
            {
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
                throw new Exception("An error occurred while adding the vehicle.", ex);
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
                throw new Exception("An error occurred while deleting the vehicle.", ex);
            }
        }

        private bool VehicleExists(int vehicleId)
        {
            return _context.Vehicles.Any(x => x.Id == vehicleId);
        }
    }
}
