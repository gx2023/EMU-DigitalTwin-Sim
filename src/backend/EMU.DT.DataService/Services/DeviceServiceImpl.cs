using EMU.DT.DataService.Data;
using EMU.DT.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Services
{
    public class DeviceServiceImpl : IDeviceService
    {
        private readonly DataDbContext _dbContext;

        public DeviceServiceImpl(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            return await _dbContext.Devices.ToListAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            return await _dbContext.Devices.FindAsync(id);
        }

        public async Task<Device> CreateDeviceAsync(Device device)
        {
            _dbContext.Devices.Add(device);
            await _dbContext.SaveChangesAsync();
            return device;
        }

        public async Task<Device> UpdateDeviceAsync(Device device)
        {
            _dbContext.Devices.Update(device);
            await _dbContext.SaveChangesAsync();
            return device;
        }

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var device = await _dbContext.Devices.FindAsync(id);
            if (device == null)
            {
                return false;
            }

            _dbContext.Devices.Remove(device);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<DeviceStatus>> GetDeviceStatusesAsync(int deviceId)
        {
            return await _dbContext.DeviceStatuses
                .Where(ds => ds.DeviceId == deviceId)
                .OrderByDescending(ds => ds.Timestamp)
                .ToListAsync();
        }

        public async Task<DeviceStatus> AddDeviceStatusAsync(DeviceStatus status)
        {
            _dbContext.DeviceStatuses.Add(status);
            await _dbContext.SaveChangesAsync();
            
            // 更新设备的当前状态
            var device = await _dbContext.Devices.FindAsync(status.DeviceId);
            if (device != null)
            {
                device.Status = status.Status;
                await _dbContext.SaveChangesAsync();
            }

            return status;
        }

        public async Task<List<DeviceMaintenance>> GetDeviceMaintenancesAsync(int deviceId)
        {
            return await _dbContext.DeviceMaintenances
                .Where(dm => dm.DeviceId == deviceId)
                .OrderByDescending(dm => dm.StartDate)
                .ToListAsync();
        }

        public async Task<DeviceMaintenance> CreateDeviceMaintenanceAsync(DeviceMaintenance maintenance)
        {
            _dbContext.DeviceMaintenances.Add(maintenance);
            await _dbContext.SaveChangesAsync();
            return maintenance;
        }

        public async Task<DeviceMaintenance> UpdateDeviceMaintenanceAsync(DeviceMaintenance maintenance)
        {
            _dbContext.DeviceMaintenances.Update(maintenance);
            await _dbContext.SaveChangesAsync();
            return maintenance;
        }
    }
}