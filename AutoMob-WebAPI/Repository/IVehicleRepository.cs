using AutoMob_WebAPI.Models;

namespace AutoMob_WebAPI.Repository
{
    public interface IVehicleRepository
    {
        IEnumerable<VehicleModel> GetAllVehicles();
        VehicleModel GetVehicleById(int vehicleId);
        void AddVehicle(VehicleModel vehicle);
        bool UpdateVehicle(VehicleModel vehicle);
        bool DeleteVehicle(int vehicleId);

    }
}
