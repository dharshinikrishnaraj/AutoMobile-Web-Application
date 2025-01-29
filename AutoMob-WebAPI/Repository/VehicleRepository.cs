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
            return _context.Vehicles.ToList();
        }

        public VehicleModel GetVehicleById(int vehicleId)
        {
            return _context.Vehicles.Find(vehicleId);
        }

        public void AddVehicle(VehicleModel vehicle)
        {
             vehicle.Id = 0; // Id is an Identity column, Ensure EF Core does not treat it as an explicit value
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void UpdateVehicle(VehicleModel vehicle)
        {
            if (VehicleExists(vehicle.Id))  //Find if the vehicle exists, if exists, update or create
            {
                _context.Vehicles.Update(vehicle);
            } 
            else
            {
                vehicle.Id = 0; // Id is an Identity column, Ensure EF Core does not treat it as an explicit value
                _context.Vehicles.Add(vehicle);
            }
            _context.SaveChanges();
        }

        public void DeleteVehicle(int vehicleId)
        {
            var vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            
            }
        }

        private bool VehicleExists(int vehicleId)
        {
            return _context.Vehicles.Any(x => x.Id == vehicleId);
        }
    }
}
