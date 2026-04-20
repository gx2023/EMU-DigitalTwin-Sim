using EMU.DT.DataService.Models;

namespace EMU.DT.DataService.Services
{
    public interface IMaintenanceService
    {
        Task<List<MaintenanceTask>> GetAllMaintenanceTasksAsync();
        Task<MaintenanceTask> GetMaintenanceTaskByIdAsync(int id);
        Task<MaintenanceTask> CreateMaintenanceTaskAsync(MaintenanceTask task);
        Task<MaintenanceTask> UpdateMaintenanceTaskAsync(MaintenanceTask task);
        Task<bool> DeleteMaintenanceTaskAsync(int id);
        Task<List<MaintenanceTask>> GetMaintenanceTasksByVehicleIdAsync(int vehicleId);
        Task<List<MaintenanceStep>> GetMaintenanceStepsAsync(int maintenanceTaskId);
        Task<MaintenanceStep> CreateMaintenanceStepAsync(MaintenanceStep step);
        Task<MaintenanceStep> UpdateMaintenanceStepAsync(MaintenanceStep step);
        Task<List<QualityCheck>> GetQualityChecksAsync(int maintenanceStepId);
        Task<QualityCheck> CreateQualityCheckAsync(QualityCheck check);
    }
}