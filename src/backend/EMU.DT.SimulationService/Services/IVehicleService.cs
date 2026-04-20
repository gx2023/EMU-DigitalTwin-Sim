using System.Collections.Generic;

namespace EMU.DT.SimulationService
{
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int vehicleId);
        Vehicle CreateVehicle(Vehicle vehicle);
        Vehicle UpdateVehicle(Vehicle vehicle);
        bool DeleteVehicle(int vehicleId);
        List<VehicleStatus> GetVehicleStatus(int vehicleId);
        VehicleStatus UpdateVehicleStatus(VehicleStatus status);
        List<MaintenanceTask> GetVehicleMaintenanceTasks(int vehicleId);
    }

    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public int Mileage { get; set; }
        public System.DateTime LastMaintenanceDate { get; set; }
        public System.DateTime NextMaintenanceDate { get; set; }
        public List<string> Tags { get; set; }
    }

    public class VehicleStatus
    {
        public int StatusId { get; set; }
        public int VehicleId { get; set; }
        public string Status { get; set; }
        public int Speed { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int BatteryLevel { get; set; }
        public System.DateTime Timestamp { get; set; }
        public List<string> Alerts { get; set; }
    }

    public class MaintenanceTask
    {
        public int TaskId { get; set; }
        public int VehicleId { get; set; }
        public string TaskType { get; set; }
        public string Status { get; set; }
        public System.DateTime ScheduledDate { get; set; }
        public System.DateTime CompletionDate { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }
        public string Notes { get; set; }
    }
}