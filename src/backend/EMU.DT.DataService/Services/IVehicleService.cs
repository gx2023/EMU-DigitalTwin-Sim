using EMU.DT.DataService.Models;

namespace EMU.DT.DataService.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(int id);
        Task<List<VehicleStatus>> GetVehicleStatusesAsync(int vehicleId);
        Task<VehicleStatus> AddVehicleStatusAsync(VehicleStatus status);
    }
}