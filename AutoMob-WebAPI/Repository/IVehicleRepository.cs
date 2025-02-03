using AutoMob_WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace AutoMob_WebAPI.Repository
{
    public interface IVehicleRepository
    {
        IEnumerable<VehicleModel> GetAllVehicles();
        VehicleModel GetVehicleById(int vehicleId);
        void AddVehicle(VehicleModel vehicle);
        bool UpdateVehicle(VehicleModel vehicle);
        bool PatchVehicle(int vehicleId, JsonPatchDocument<VehicleModel> patchDocument);
        bool DeleteVehicle(int vehicleId);

    }
}
