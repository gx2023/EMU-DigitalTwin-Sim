using EMU.DT.DataService.Data;
using EMU.DT.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Services
{
    public class VehicleServiceImpl : IVehicleService
    {
        private readonly DataDbContext _dbContext;

        public VehicleServiceImpl(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _dbContext.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return false;
            }

            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<VehicleStatus>> GetVehicleStatusesAsync(int vehicleId)
        {
            return await _dbContext.VehicleStatuses
                .Where(vs => vs.VehicleId == vehicleId)
                .OrderByDescending(vs => vs.Timestamp)
                .ToListAsync();
        }

        public async Task<VehicleStatus> AddVehicleStatusAsync(VehicleStatus status)
        {
            _dbContext.VehicleStatuses.Add(status);
            await _dbContext.SaveChangesAsync();
            
            // 更新车辆的当前状态
            var vehicle = await _dbContext.Vehicles.FindAsync(status.VehicleId);
            if (vehicle != null)
            {
                vehicle.Status = status.Status;
                await _dbContext.SaveChangesAsync();
            }

            return status;
        }
    }
}