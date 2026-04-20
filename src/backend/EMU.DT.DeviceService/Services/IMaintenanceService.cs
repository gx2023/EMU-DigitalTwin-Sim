using System.Collections.Generic;

namespace EMU.DT.DeviceService
{
    public interface IMaintenanceService
    {
        List<MaintenanceTask> GetAllMaintenanceTasks();
        MaintenanceTask GetMaintenanceTaskById(int taskId);
        MaintenanceTask CreateMaintenanceTask(MaintenanceTask task);
        MaintenanceTask UpdateMaintenanceTask(MaintenanceTask task);
        bool DeleteMaintenanceTask(int taskId);
        List<MaintenanceRecord> GetMaintenanceRecords(int deviceId);
        MaintenanceRecord CreateMaintenanceRecord(MaintenanceRecord record);
        List<MaintenancePlan> GetMaintenancePlans();
        MaintenancePlan CreateMaintenancePlan(MaintenancePlan plan);
    }

    public class MaintenanceTask
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string DeviceId { get; set; }
        public string EquipmentId { get; set; }
        public string TaskType { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public System.DateTime ScheduledDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public System.DateTime? CompletedDate { get; set; }
        public string AssignedTo { get; set; }
        public string Description { get; set; }
        public List<string> PartsRequired { get; set; }
    }

    public class MaintenanceRecord
    {
        public int RecordId { get; set; }
        public int DeviceId { get; set; }
        public int EquipmentId { get; set; }
        public string MaintenanceType { get; set; }
        public System.DateTime MaintenanceDate { get; set; }
        public string PerformedBy { get; set; }
        public string Description { get; set; }
        public List<string> PartsReplaced { get; set; }
        public string Status { get; set; }
        public double DurationHours { get; set; }
        public string Notes { get; set; }
    }

    public class MaintenancePlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string DeviceType { get; set; }
        public string EquipmentType { get; set; }
        public string MaintenanceType { get; set; }
        public int IntervalDays { get; set; }
        public List<string> Tasks { get; set; }
        public List<string> RequiredSkills { get; set; }
        public string Instructions { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }
}