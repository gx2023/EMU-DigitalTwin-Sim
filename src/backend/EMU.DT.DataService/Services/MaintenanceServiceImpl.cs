using EMU.DT.DataService.Data;
using EMU.DT.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Services
{
    public class MaintenanceServiceImpl : IMaintenanceService
    {
        private readonly DataDbContext _dbContext;

        public MaintenanceServiceImpl(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MaintenanceTask>> GetAllMaintenanceTasksAsync()
        {
            return await _dbContext.MaintenanceTasks
                .Include(mt => mt.Vehicle)
                .Include(mt => mt.ProcessCard)
                .ToListAsync();
        }

        public async Task<MaintenanceTask> GetMaintenanceTaskByIdAsync(int id)
        {
            return await _dbContext.MaintenanceTasks
                .Include(mt => mt.Vehicle)
                .Include(mt => mt.ProcessCard)
                .Include(mt => mt.Steps)
                .FirstOrDefaultAsync(mt => mt.Id == id);
        }

        public async Task<MaintenanceTask> CreateMaintenanceTaskAsync(MaintenanceTask task)
        {
            _dbContext.MaintenanceTasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<MaintenanceTask> UpdateMaintenanceTaskAsync(MaintenanceTask task)
        {
            _dbContext.MaintenanceTasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteMaintenanceTaskAsync(int id)
        {
            var task = await _dbContext.MaintenanceTasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }

            _dbContext.MaintenanceTasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<MaintenanceTask>> GetMaintenanceTasksByVehicleIdAsync(int vehicleId)
        {
            return await _dbContext.MaintenanceTasks
                .Where(mt => mt.VehicleId == vehicleId)
                .Include(mt => mt.ProcessCard)
                .OrderByDescending(mt => mt.StartDate)
                .ToListAsync();
        }

        public async Task<List<MaintenanceStep>> GetMaintenanceStepsAsync(int maintenanceTaskId)
        {
            return await _dbContext.MaintenanceSteps
                .Where(ms => ms.MaintenanceTaskId == maintenanceTaskId)
                .OrderBy(ms => ms.StepNumber)
                .Include(ms => ms.QualityChecks)
                .ToListAsync();
        }

        public async Task<MaintenanceStep> CreateMaintenanceStepAsync(MaintenanceStep step)
        {
            _dbContext.MaintenanceSteps.Add(step);
            await _dbContext.SaveChangesAsync();
            return step;
        }

        public async Task<MaintenanceStep> UpdateMaintenanceStepAsync(MaintenanceStep step)
        {
            _dbContext.MaintenanceSteps.Update(step);
            await _dbContext.SaveChangesAsync();
            return step;
        }

        public async Task<List<QualityCheck>> GetQualityChecksAsync(int maintenanceStepId)
        {
            return await _dbContext.QualityChecks
                .Where(qc => qc.MaintenanceStepId == maintenanceStepId)
                .OrderBy(qc => qc.CheckTime)
                .ToListAsync();
        }

        public async Task<QualityCheck> CreateQualityCheckAsync(QualityCheck check)
        {
            _dbContext.QualityChecks.Add(check);
            await _dbContext.SaveChangesAsync();
            return check;
        }
    }
}