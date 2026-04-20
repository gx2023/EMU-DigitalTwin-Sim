using EMU.DT.DataService.Models;

namespace EMU.DT.DataService.Services
{
    public interface IDeviceService
    {
        Task<List<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task<Device> CreateDeviceAsync(Device device);
        Task<Device> UpdateDeviceAsync(Device device);
        Task<bool> DeleteDeviceAsync(int id);
        Task<List<DeviceStatus>> GetDeviceStatusesAsync(int deviceId);
        Task<DeviceStatus> AddDeviceStatusAsync(DeviceStatus status);
        Task<List<DeviceMaintenance>> GetDeviceMaintenancesAsync(int deviceId);
        Task<DeviceMaintenance> CreateDeviceMaintenanceAsync(DeviceMaintenance maintenance);
        Task<DeviceMaintenance> UpdateDeviceMaintenanceAsync(DeviceMaintenance maintenance);
    }
}